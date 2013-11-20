using com.riotgames.team;

namespace com.riotgames.platform.statistics.team
{
    public class TeamPlayerAggregatedStatsDTO
    {
        public int playerId;
        public TeamId teamId;
        public TeamStatType teamStatType;

        public AggregatedStats aggregatedStats;
    }
}