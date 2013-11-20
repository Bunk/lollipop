using System.Threading.Tasks;
using com.riotgames.platform.leagues;
using com.riotgames.platform.leagues.client.dto;
using com.riotgames.platform.leagues.pojo;
using com.riotgames.team;
using com.riotgames.team.dto;

namespace Lollipop.Services
{
    public interface ILeagueService
    {
        Task<PlayerDTO> GetPlayer(int summonerId);

        Task<SummonerLeaguesDTO> GetAllLeagues(int summonerId);

        Task<TeamDTO> GetTeam(TeamId teamId);

        Task<TeamDTO> GetTeamByTag(string tag);

        Task<TeamDTO> GetTeamByName(string tag);

        Task<SummonerLeaguesDTO> GetAllLeagues(TeamId teamId);

        Task<LeagueListDTO> GetChallengerLeague(LeagueQueue queue);
    }
}