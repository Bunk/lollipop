using System.Collections.Generic;
using System.Threading.Tasks;
using com.riotgames.platform.statistics;
using com.riotgames.platform.statistics.team;
using com.riotgames.team;
using Lollipop.Session;

namespace Lollipop.Services
{
    public class StatsService : IStatsService
    {
        private readonly ILeagueConnection _conn;

        public StatsService(ILeagueConnection connection)
        {
            _conn = connection;
        }

        public Task<PlayerLifeTimeStats> GetLifetimeStats(int accountId)
        {
            return _conn.Call<PlayerLifeTimeStats>("playerStatsService", "retrievePlayerStatsByAccountId",
                accountId, "CURRENT");
        }

        public Task<AggregatedStats> GetAggregatedStats(int accountId, GameMode mode)
        {
            return _conn.Call<AggregatedStats>("playerStatsService", "getAggregatedStats",
                accountId, mode.ToString(), "CURRENT");
        }

        public Task<List<TeamAggregatedStatsDTO>> GetAggregatedStats(TeamId teamId)
        {
            return _conn.Call<List<TeamAggregatedStatsDTO>>("playerStatsService", "getTeamAggregatedStats", teamId);
        }

        public Task<List<ChampionStatInfo>> GetMostPlayedChamps(int accountId, GameMode mode)
        {
            return _conn.Call<List<ChampionStatInfo>>("playerStatsService", "retrieveTopPlayedChampions",
                accountId, mode.ToString());
        }

        public Task<RecentGames> GetRecentGames(int accountId)
        {
            return _conn.Call<RecentGames>("playerStatsService", "getRecentGames", accountId);
        }

        public Task<EndOfGameStats> GetGameStats(TeamId teamId, int gameId)
        {
            return _conn.Call<EndOfGameStats>("playerStatsService", "getTeamEndOfGameStats", teamId, gameId);
        }
    }
}