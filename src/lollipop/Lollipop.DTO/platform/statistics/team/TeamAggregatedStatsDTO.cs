using System.Collections.Generic;
using com.riotgames.platform.leagues;
using com.riotgames.team;

namespace com.riotgames.platform.statistics.team
{
    public class TeamAggregatedStatsDTO
    {
        public TeamId teamId;
        public LeagueQueue queueType;

        public string serializedToJson;

        public List<TeamPlayerAggregatedStatsDTO> playerAggregatedStatsList;

        public TeamAggregatedStatsDTO()
        {
            playerAggregatedStatsList = new List<TeamPlayerAggregatedStatsDTO>();
        }
    }
}