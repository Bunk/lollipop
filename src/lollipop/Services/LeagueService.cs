using System.Threading.Tasks;
using com.riotgames.platform.leagues;
using com.riotgames.platform.leagues.client.dto;
using com.riotgames.platform.leagues.pojo;
using com.riotgames.team;
using com.riotgames.team.dto;
using Lollipop.Session;

namespace Lollipop.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueAccount _conn;

        public LeagueService(ILeagueAccount connection)
        {
            _conn = connection;
        }

        public Task<PlayerDTO> GetPlayer(int summonerId)
        {
            return _conn.Call<PlayerDTO>("summonerTeamService", "findPlayer", summonerId);
        }

        public Task<SummonerLeaguesDTO> GetAllLeagues(int summonerId)
        {
            return _conn.Call<SummonerLeaguesDTO>("leaguesServiceProxy", "getAllLeaguesForPlayer", summonerId);
        }

        public Task<TeamDTO> GetTeam(TeamId teamId)
        {
            return _conn.Call<TeamDTO>("summonerTeamService", "findTeamById", teamId);
        }

        public Task<TeamDTO> GetTeamByTag(string tag)
        {
            return _conn.Call<TeamDTO>("summonerTeamService", "findTeamByTag", tag);
        }

        public Task<TeamDTO> GetTeamByName(string tag)
        {
            return _conn.Call<TeamDTO>("summonerTeamService", "findTeamByName", tag);
        }

        public Task<SummonerLeaguesDTO> GetAllLeagues(TeamId teamId)
        {
            return _conn.Call<SummonerLeaguesDTO>("leaguesServiceProxy", "getLeaguesForTeam", teamId.fullId);
        }

        public Task<LeagueListDTO> GetChallengerLeague(LeagueQueue queue)
        {
            return _conn.Call<LeagueListDTO>("leaguesServiceProxy", "getChallengerLeague", queue);
        }
    }
}