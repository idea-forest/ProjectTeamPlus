using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SteamProject.DAL.Abstract;
using SteamProject.Models;
using Microsoft.AspNetCore.Identity;
using SteamProject.Services;
using Microsoft.AspNetCore.Authorization;
using SteamProject.Models.DTO;
using SteamProject.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace SteamProject.Controllers;

public class LibraryController: Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly ISteamService _steamService;
    private readonly IUserGameInfoRepository _userGameInfoRepository;

    public LibraryController(UserManager<IdentityUser> userManager, IUserRepository userRepository, IGameRepository gameRepository, IUserGameInfoRepository userGameInfoRepository, ISteamService steamService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _steamService = steamService;
        _userGameInfoRepository = userGameInfoRepository;
    }

    [Authorize]
    public async Task<IActionResult> Index(bool refresh)
    {
        string? id = _userManager.GetUserId(User);

        if (refresh == null)
        {
            refresh = false;
        }

        var userLibraryVM = new UserLibraryVM();

        if(id is null)
        {
            return View();
        }
        else
        {
            User user = _userRepository.GetUser(id);

            if (user.AvatarUrl == null && user.SteamId != null)
            {
                var steamUser = _steamService.GetSteamUser(user.SteamId);
                user.SteamName = steamUser.SteamName;
                user.AvatarUrl = steamUser.AvatarUrl;
                var SteamLevel = _steamService.GetUserLevel(user.SteamId);

                user.PlayerLevel = SteamLevel;

                _userRepository.AddOrUpdate(user);
            }

            userLibraryVM._user = user;
            List<UserGameInfo> gameInfo = _userGameInfoRepository.GetAllUserGameInfo(user.Id);
            userLibraryVM._games = new HashSet<Game>();
            UserGameInfo? currentUserInfo = new UserGameInfo();

            if (gameInfo.Count == 0)
            {
                refresh = true;
            }
            if (user.SteamId == null)
            {
                return View();
            }
            if(refresh == true)
            {
                List<Game>? games = _steamService.GetGames(user.SteamId, user.Id).ToList();
                
                if(games.Count == 0)
                    return View();

                // Checks to see  if each individual game from Steam is in our db or not on a library refresh
                // --- Refresh isn't the page refreshing, it's the user initiating a refresh to get newly added games
                Game currentGame = new Game();
                foreach(var game in games)
                {
                    currentGame = _gameRepository.GetGameByAppId(game.AppId);
                    if(currentGame == null)
                    {
                        try
                        {
                            currentGame = _gameRepository.GetGameByAppId(game.AppId);
                            var tempGenreString = "";
                            if(currentGame != null && currentGame.Genres == null)
                            {
                                try
                                {
                                    var genreResults = await _steamService.GetGameInfoAsync(game.Name);
                                    foreach(var genre in genreResults)
                                    {
                                        tempGenreString += genre + ",";
                                    }
                                    currentGame.Genres = tempGenreString.Substring(0, (tempGenreString.Length - 1));
                                }
                                catch
                                {
                                    tempGenreString = "The genres couldn't be grabbed";
                                }
                                
                                if(currentGame.Genres == null)
                                {
                                    currentGame.Genres = "The genres couldn't be grabbed";
                                }
                            }

                            int? playTime = game.PlayTime;
                            int? lastPlayed = game.LastPlayed;

                            game.PlayTime = 0;
                            game.LastPlayed = 0;

                            try
                            {
                                currentUserInfo = _userGameInfoRepository.GetUserInfoForGame( game.AppId, user.Id );
                            }
                            catch
                            {
                                currentUserInfo = null;
                            }
                            //Check if game is in database, if not add it
                            if (game.Genres == null)
                            {
                                try
                                {
                                    var genreResults = await _steamService.GetGameInfoAsync(game.Name);
                                    foreach(var genre in genreResults)
                                    {
                                        tempGenreString += genre + ",";
                                    }
                                    game.Genres = tempGenreString.Substring(0, (tempGenreString.Length - 1));
                                }
                                catch
                                {
                                    tempGenreString = "The genres couldn't be grabbed";
                                }
                                
                                if(game.Genres == null)
                                {
                                    game.Genres = "The genres couldn't be grabbed";
                                }
                                _gameRepository.AddOrUpdate(game);

                                var temp = _gameRepository.GetAll(g => g.AppId == game.AppId).FirstOrDefault();
                                if (currentUserInfo == null)
                                {
                                    
                                    _userGameInfoRepository.AddOrUpdate(new UserGameInfo{
                                        OwnerId = user.Id,
                                        GameId = temp.Id,
                                        PlayTime = playTime,
                                        LastPlayed = lastPlayed,
                                        Hidden = false,
                                        Followed = false,
                                        Game = game,
                                        Owner = user
                                    });
                                    userLibraryVM._games.Add(game);
                                }
                                else
                                {
                                    UserGameInfo currentGameInfo = gameInfo.Single(g => g.GameId == temp.Id);
                                    currentGameInfo.LastPlayed = lastPlayed;
                                    currentGameInfo.PlayTime = playTime;
                                    _userGameInfoRepository.AddOrUpdate(currentGameInfo);
                                    userLibraryVM._games.Add(game);
                                }
                            }
                            else
                            {
                                if (currentUserInfo == null)
                                {

                                    UserGameInfo newInfo = new UserGameInfo
                                    {
                                        OwnerId = user.Id,
                                        GameId = currentGame.Id,
                                        PlayTime = playTime,
                                        LastPlayed = lastPlayed,
                                        Hidden = false,
                                        Followed = false,
                                        Owner = user,
                                        Game = currentGame
                                    };
                                    _userGameInfoRepository.AddOrUpdate(newInfo);
                                    userLibraryVM._games.Add(game);

                                }
                                else
                                {
                                    UserGameInfo currentGameInfo = gameInfo.Single(g => g.GameId == currentGame.Id);
                                    currentGameInfo.LastPlayed = lastPlayed;
                                    currentGameInfo.PlayTime = playTime;
                                    _userGameInfoRepository.AddOrUpdate(currentGameInfo);
                                    userLibraryVM._games.Add(game);
                                }
                            }
                        }
                        catch
                        {
                            throw new Exception("Current game couldn't be saved to the db!" + game.Name);
                        }
                    }
                }
            }
            else
            {
                userLibraryVM._games = _gameRepository.GetGamesListByUserInfo(gameInfo);
            }
            userLibraryVM._user.UserGameInfos = userLibraryVM._user.UserGameInfos.OrderBy(g => g.Game.Name).ToList();
            return View(userLibraryVM);
        }
    }
    public IActionResult ShowMoreInfo(int appId)
    {
        string? id = _userManager.GetUserId(User);
        User user = _userRepository.GetUser(id);

        Game game = _gameRepository.GetGameByAppId(appId);
        GameVM gameVM = _steamService.GetGameInfo(game);

        gameVM._game = game;
        gameVM._appId = appId;
        gameVM._userGame = _userGameInfoRepository.GetAll(g => g.GameId == game.Id).FirstOrDefault();
        
        gameVM.playTime = Math.Round(Convert.ToDouble(gameVM._userGame.PlayTime)/60, 1);

        gameVM.cleanRequirements();
        gameVM.cleanDescriptions();

        return View(gameVM);
    }
}