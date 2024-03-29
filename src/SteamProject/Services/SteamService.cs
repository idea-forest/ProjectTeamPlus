using SteamProject.Models;
using SteamProject.Models.DTO;
using SteamProject.Helpers;
using System.Text.Json;
using System.Text.RegularExpressions;
using SteamProject.ViewModels;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SteamProject.Services;

public class SteamService : ISteamService
{
    public static readonly HttpClient _httpClient = new HttpClient();
    string Token;
    string AdminToken;
    private readonly string _clientId;
    private readonly string _accessToken;

    // This is the shared account for testing the admin and library funcitons with a small set of games.
    // If you want to use the whole library, comment this line out and uncomment the other BulkUserSTeamId
    //  which has the number ending in 720 -- It's line 33 right now that but that may change
    //string BulkUserSteamId = "76561199495917967";
    

    // This Steam account is Justin's personal one with 240ish games. In the future we
    //  could change this to use a larger one, but for now it's all that's needed.
    // If you plan on using the shared account for testing, comment this out and uncomment the other 
    // BulkUserSTeamId which has the number ending in 967 -- It's line 26 right now but that may change
    string BulkUserSteamId = "76561198070063720";

    
    public SteamService( string token, string adminToken, string clientId, string accessToken )
    {
        Token = token;
        AdminToken = adminToken;
        _clientId = clientId;
        _accessToken = accessToken;
    }


    public string GetJsonStringFromEndpoint(string uri)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage( HttpMethod.Get, uri );
        var response = client.Send(request);
        if (response.IsSuccessStatusCode)
        {
            // Note there is only an async version of this so to avoid forcing you to use all async I'm waiting for the result manually
            string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return responseText;
        }
        else
        {
            // What to do if failure? 401? Should throw and catch specific exceptions that explain what happened
            return null;
        }
    }

    public User GetSteamUser(string steamid)
    {
        string uri = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={Token}&steamids={steamid}";
        string? jsonResponse = GetJsonStringFromEndpoint( uri );

        var poco = JsonSerializer.Deserialize<SteamUserPOCO>(jsonResponse);

        var returnMe = new User();
        returnMe.TakeSteamPOCO(poco);

        return returnMe;
    }

    public List<User> GetManyUsers( List<string> steamIds )
    {
        string uri = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={Token}&steamids=";

        foreach( string id in steamIds )
        {
            uri += id;
            uri += ",";
        }

        string? jsonResponse = GetJsonStringFromEndpoint( uri );

        var poco = JsonSerializer.Deserialize<SteamUserPOCO>(jsonResponse);

        var userList = new List<User>();
        foreach( var id in steamIds )
        {
            var userOut = new User();
            userOut.TakeSteamPOCO( poco );
            userList.Add(userOut);

            poco.response.players.RemoveAt( 0 );
        }

        return userList;
    }

    public int GetUserLevel(string steamid)
    {
        string uri = $"https://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key={Token}&steamid={steamid}";
        string? jsonResponse = GetJsonStringFromEndpoint( uri );

        var returnMe = JsonSerializer.Deserialize<UserLevelPOCO>(jsonResponse).response.player_level;

        return returnMe;
    }

    public List<Friend> GetFriendsList(string steamid, int userId)
    {
        string friendsListUri = $"https://api.steampowered.com/ISteamUser/GetFriendList/v1/?key={Token}&steamid={steamid}";
        string? jsonResponse = GetJsonStringFromEndpoint( friendsListUri );

        //breaks here
        var friendPocoList = JsonSerializer.Deserialize<FriendsListPOCO>(jsonResponse).friendslist.friends;

        var idList = new List<string>();
        foreach( var friend in friendPocoList )
        {
            idList.Add( friend.steamid );
        }

        string friendIdsParam = "";
        foreach( var id in idList )
        {
            friendIdsParam += $"{id},";
        }

        string friendsInfoUri = $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={Token}&steamids={friendIdsParam}";
        jsonResponse = GetJsonStringFromEndpoint( friendsInfoUri );

        var FriendsData = JsonSerializer.Deserialize<SteamUserPOCO>(jsonResponse).response.players;
        var FriendsList = new List<Friend>();
        foreach( var friend in FriendsData )
        {
            var FriendOut = new Friend();
            FriendOut.TakePlayerPOCO(friend);
            FriendOut.RootId = userId;
            FriendsList.Add(FriendOut);
        }

        return FriendsList.OrderBy( f => f.SteamName ).ToList<Friend>();
    }

    public Friend GetFriendSpecific( string userSteamId, int userId, string friendSteamId )
    {
        var returnMe = GetFriendsList(userSteamId, userId).Where( f => f.SteamId == friendSteamId ).FirstOrDefault();

        return returnMe;
    }

    public IEnumerable<Game> GetGames(string userSteamId, int userId)
    {
        string source = string.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json&include_appinfo=1", Token, userSteamId);
        return GetGamesGeneric(source, userId);
    }

    public IEnumerable<Game> GetSteamCuratorGames()
    {
        // This will use the shared team Steam account for now so it's a small list to work with for testing.
        string source = string.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json&include_appinfo=1", AdminToken, BulkUserSteamId);
        return GetGamesGeneric(source, 1);
    }

    public IEnumerable<Game> GetGamesGeneric(string source, int userId)
    {
        string jsonResponse = GetJsonStringFromEndpoint(source);
        if(jsonResponse == null)
            return null;
        else
        {
            var poco = JsonSerializer.Deserialize<LibraryPOCO>(jsonResponse);
            var games = new List<Game>();
            if( poco.response.games is not null )
            {
                if( poco.response.games.Count() != 0 )
                {
                    foreach(var game in poco.response.games)
                    {
                        var temp = new Game();
                        temp = temp.TakeLibraryInfoPOCO(game, userId);
                        games.Add(temp);
                    }
                }
            }
            return games.OrderBy(g => g.Name);
        }
    }

    public IEnumerable<Game> GetSharedGames( string userSteamId, string friendSteamId, int userId )
    {
        var myGames = new List<Game>();
        myGames = GetGames( userSteamId, userId ).ToList<Game>();

        var friendsGames = new List<Game>();
        friendsGames = GetGames( friendSteamId, 0 ).ToList<Game>();

        var sharedGames = new List<Game>();
        sharedGames = myGames.Join(friendsGames, g1 => g1.AppId, g2 => g2.AppId, (g1, g2) => g1 ).ToList<Game>();

        return sharedGames.OrderBy( g => g.Name );
    }

    public GameVM GetGameInfo(Game game)
    {
        var gameVM = new GameVM();
        string source = string.Format("https://store.steampowered.com/api/appdetails?appids={0}&l=en", game.AppId);
        string jsonResponse = GetJsonStringFromEndpoint(source);
        
        if(jsonResponse != null)
        {   
            var regex = new Regex(Regex.Escape(game.AppId.ToString()));
            jsonResponse = regex.Replace(jsonResponse, "response", 1);
            try
            {
                regex = new Regex(Regex.Escape("linux_requirements\":[]"));
                jsonResponse = regex.Replace(jsonResponse, "linux_requirements\":{}", 1);
            }
            catch {}
            try
            {
                regex = new Regex(Regex.Escape("mac_requirements\":[]"));
                jsonResponse = regex.Replace(jsonResponse, "mac_requirements\":{}", 1);
            }
            catch {}
            try
            {
                regex = new Regex(Regex.Escape("pc_requirements\":[]"));
                jsonResponse = regex.Replace(jsonResponse, "pc_requirements\":{}", 1);
            }
            catch {}
            gameVM._poco = JsonSerializer.Deserialize<GameInfoPOCO>(jsonResponse);
        }
        return gameVM;
    }
    public async Task<HashSet<string>> GetGameInfoAsync(string gameName)
    {
        // Games like Titanfall 2 have a trademark symbol from Steam, so IGDB doesn't understand what they are.
        string namePattern = @"[^\w\s]";
        gameName = Regex.Replace(gameName, namePattern, "");

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Client-ID", _clientId);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var body = $"fields genres.name; search \"{gameName}\";";
            var response = await client.PostAsync("https://api.igdb.com/v4/games", new StringContent(body));

            while (response.StatusCode ==  System.Net.HttpStatusCode.TooManyRequests)
            {
                Thread.Sleep(1000);
                response = await client.PostAsync("https://api.igdb.com/v4/games", new StringContent(body));
            }
            var content = await response.Content.ReadAsStringAsync();
            if(content == null)
                return null;
            else
            {
                try
                {
                    // These regex are to remove the /n and white spaces that the json comes with
                    string pattern = @"\s+";
                    content = Regex.Replace(content, pattern, "");

                    pattern = @"\""genres\""\s*:";
                    string replacement = "\"genreCategory\":";
                    content = Regex.Replace(content, pattern, replacement);

                    // Needed to wrap the json in something so I could parse it correctly.
                    content = $"{{\"MyArray\":{content}}}";

                    var genrePOCO = JsonSerializer.Deserialize<GenrePOCO>(content);

                    var genreNames = new HashSet<string>();
                    foreach(var genreCategory in genrePOCO.MyArray)
                    {
                        if(genreCategory.genreCategory != null)
                        {
                            foreach(var genre in genreCategory.genreCategory)
                            {
                                genreNames.Add(genre.name);
                            }
                        }
                    }
                    return genreNames;
                }
                catch 
                {
                    return null;
                }
            }
        }
    }

    public async Task<HashSet<GameGenresPOCO>> GetGenresAsync()
    {
        HashSet<GameGenresPOCO> genres = new HashSet<GameGenresPOCO>();
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Client-ID", _clientId);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");

            var queryString = "fields=name";
            var response = await client.GetAsync($"https://api.igdb.com/v4/genres?{queryString}");

            var content = await response.Content.ReadAsStringAsync();

            if(content == null)
                return null;
            else
            {
                try
                {
                    content = Regex.Replace(content, @"[\s\n]+", "");

                    var genresFromJson = JsonSerializer.Deserialize<List<GameGenresPOCO>>(content);
                    foreach(var genre in genresFromJson)
                    {
                        genres.Add(genre);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return genres;
        }
    }

    public GameNewsVM GetGameNews(Game game, int count = 10)
    {
        var gameVM = new GameNewsVM();
        int fetchCount = 2+count * 2; // Fetch more items to account for the SteamDb items
        string source = string.Format("https://api.steampowered.com/ISteamNews/GetNewsForApp/v0002/?appid={0}&count={1}&l=en", game.AppId, fetchCount);
        string jsonResponse = GetJsonStringFromEndpoint(source);

        if (jsonResponse != null)
        {
            var newsPoco = JsonSerializer.Deserialize<GameNewsPoco>(jsonResponse);
            var filteredNewsItems = new List<Newsitem>();

            for (var i = 0; i < newsPoco.appnews.newsitems.Count && filteredNewsItems.Count < count; i++)
            {
                var newsItem = newsPoco.appnews.newsitems[i];

                if (newsItem.feedlabel.ToUpper() != "SteamDb".ToUpper())
                {
                    newsItem.contents = HelperMethods.StripJunkFromString(newsItem.contents);
                    newsItem.dateTime = HelperMethods.UnixTimeStampToDateTime(newsItem.date);
                    filteredNewsItems.Add(newsItem);
                }
                
            }

            // Limit the results to the requested count
            newsPoco.appnews.newsitems = filteredNewsItems.Take(count).ToList();
            gameVM._poco = newsPoco;
        }

        return gameVM;
    }

    public AchievementRoot GetAchievements(string userSteamId, int appId)
    {
        string source = string.Format("http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v1/?appid={0}&key={1}&steamid={2}&l=en", appId, Token, userSteamId);
        string response = GetJsonStringFromEndpoint(source);
        if (response == null)
        {
            return null;
        }
        AchievementRoot deserialized = JsonSerializer.Deserialize<AchievementRoot>(response)!;
        return deserialized;
    }

    public List<Achievement> GetSharedMissingAchievements( string userSteamId, string friendSteamId, int appId )
    {
        var userAchievementResult = new AchievementRoot();
        userAchievementResult = GetAchievements( userSteamId, appId );

        var friendAchievementResult = new AchievementRoot();
        friendAchievementResult = GetAchievements( friendSteamId, appId );

        var userAchList = new List<Achievement>();
        if( userAchievementResult != null )
            userAchList = userAchievementResult.playerstats.achievements;

        var friendAchList = new List<Achievement>();
        if( friendAchievementResult != null )
            friendAchList = friendAchievementResult.playerstats.achievements;

        var sharedMissingAchievements = new List<Achievement>();
        if( userAchList != null )
            foreach( var userAch in userAchList )
            {
                foreach( var friendAch in friendAchList )
                {
                    if( userAch.apiname == friendAch.apiname )
                    {
                        if( userAch.achieved == friendAch.achieved )
                        {
                            if( userAch.achieved == 0 )
                                sharedMissingAchievements.Add( userAch );
                        }
                    }
                }
            }   

        return sharedMissingAchievements.OrderBy( ach => ach.name ).ToList<Achievement>();
    }

    public SchemaRoot GetSchema(int appId)
        {
            string source = string.Format("https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v2/?appid={0}&key={1}&l=en", appId, Token);
            string response = GetJsonStringFromEndpoint(source);
            if (response == null)
            {
                return null;
            }
            // IEnumerable<Achievement> userAchievements = JsonSerializer.Deserialize<IEnumerable<Achievement>>(response)!;
            SchemaRoot deserialized = JsonSerializer.Deserialize<SchemaRoot>(response)!;
            return deserialized;
        }

    public GAPRoot GetGAP(int appId)
    {
        string source = string.Format("https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid={0}&format=json", appId);
        string response = GetJsonStringFromEndpoint(source);
        if (response == null)
        {
            return null;
        }
        GAPRoot deserialized = JsonSerializer.Deserialize<GAPRoot>(response)!;
        return deserialized;
    }
}