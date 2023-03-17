using Microsoft.EntityFrameworkCore;
using SteamProject.DAL.Abstract;
using SteamProject.DAL.Concrete;
using SteamProject.Models;

namespace SteamProject.DAL.Concrete;

public class CompetitionRepository : Repository<Competition>,  ICompetitionRepository
{
    public CompetitionRepository(SteamInfoDbContext ctx) : base(ctx)
    {

    }

    public Competition GetCompetitionById(int id)
    {
        return GetAll().Where( c => c.Id == id ).FirstOrDefault();
    }

    public Competition GetCompetitionByCompPlayerAndGameId( CompetitionPlayer player, int gameId )
    {
        return GetAll().Where( c => c.GameId == gameId && c.CompetitionPlayers.Contains( player ) ).FirstOrDefault();
    }

    public List<Competition> GetAllCompetitionsForUser( List<CompetitionPlayer> entries )
    {
        if ( entries == null || entries.Count() == 0 )
            return null;
        
        var returnMe = new List<Competition>();
        foreach( var competitionPlayer in entries )
        {
            var CompsFound = new List<Competition>();
            CompsFound = GetAll().Where( c => c.CompetitionPlayers.Contains( competitionPlayer ) ).ToList<Competition>();
            foreach( var comp in CompsFound )
                returnMe.Add( comp );
        }

        return returnMe;
    }

}