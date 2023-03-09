using SteamProject.DAL.Abstract;
using SteamProject.Models;

namespace SteamProject.DAL.Abstract
{

    public interface IUserGameInfoRepository : IRepository<UserGameInfo>
    {
        UserGameInfo? GetUserInfoForGame(int gameAppId, int userId );

        List<UserGameInfo> GetAllUserGameInfo(int userId);

    }
}