using SteamProject.Models;

namespace SteamProject.DAL.Abstract;

public interface ICompetitionRepository : IRepository<Competition>
{
    public Competition GetCompetitionById( int id );

    public Competition GetCompetitionByCompPlayerAndGameId( CompetitionPlayer player, int gameId );

    public List<Competition> GetAllCompetitionsForUser( List<CompetitionPlayer> entries );

    public List<Competition> GetCurrentCompetitionsBySteamId(string steamId);
    public List<Competition> GetPreviousCompetitionsBySteamId(string steamId);


}