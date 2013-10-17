using System.Collections.Generic;
using System.Threading.Tasks;
using com.riotgames.platform.statistics;
using com.riotgames.platform.statistics.team;
using com.riotgames.team;

namespace Lollipop.Services
{
    public interface IStatsService
    {
        Task<PlayerLifeTimeStats> GetLifetimeStats(int accountId);

        Task<AggregatedStats> GetAggregatedStats(int accountId, GameMode mode);

        Task<List<TeamAggregatedStatsDTO>> GetAggregatedStats(TeamId teamId);
        
        Task<List<ChampionStatInfo>> GetMostPlayedChamps(int accountId, GameMode mode);

        Task<RecentGames> GetRecentGames(int accountId);

        Task<EndOfGameStats> GetGameStats(TeamId teamId, int gameId);
    }
}