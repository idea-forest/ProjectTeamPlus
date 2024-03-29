using SteamProject.Models;
using SteamProject.Models.DTO;
using SteamProject.ViewModels;
using Newtonsoft.Json.Linq;

namespace SteamProject.Services;

public interface ISteamService
{
    User GetSteamUser(string steamid);
    public List<User> GetManyUsers( List<string> steamIds );
    int GetUserLevel(string steamid);
    List<Friend> GetFriendsList(string steamid, int userId);
    public Friend GetFriendSpecific( string userSteamId, int userId, string friendSteamId );
    IEnumerable<Game> GetGames(string userSteamId, int userId);
    IEnumerable<Game> GetSharedGames( string userSteamId, string friendSteamId, int userId );
    IEnumerable<Game> GetGamesGeneric(string source, int userId);
    IEnumerable<Game> GetSteamCuratorGames();
    GameNewsVM GetGameNews(Game game, int count = 10);
    GameVM GetGameInfo(Game game);
    Task<HashSet<string>> GetGameInfoAsync(string gameName);
    Task<HashSet<GameGenresPOCO>> GetGenresAsync();
    AchievementRoot GetAchievements(string userSteamId, int appId);
    public List<Achievement> GetSharedMissingAchievements( string userSteamId, string friendSteamId, int appId );
    SchemaRoot GetSchema(int appId);
    GAPRoot GetGAP(int appId);
}