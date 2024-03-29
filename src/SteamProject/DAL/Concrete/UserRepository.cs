using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SteamProject.DAL.Abstract;
using SteamProject.Data;
using SteamProject.Models;

namespace SteamProject.DAL.Concrete;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly SteamInfoDbContext _ctx;

    public UserRepository(SteamInfoDbContext ctx) : base(ctx)
    {
        _ctx = ctx;
    }

    public User GetUser(string userId)
    {
        var currentUser = GetAll().FirstOrDefault(u => u.AspNetUserId == userId);

        return currentUser;
    }

    public void UpdateUserTheme(int userId, string theme)
    {
        var user = _ctx.Users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.Theme = theme;
            _ctx.SaveChanges();
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        return GetAll().ToList();
    }
}