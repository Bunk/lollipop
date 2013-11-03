using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class PlayerStatSummaries : VersionedObject
    {
        public int userId;

        public int season;

        public List<PlayerStatSummary> playerStatSummarySet;

        public PlayerStatSummaries()
        {
            playerStatSummarySet = new List<PlayerStatSummary>();
        }
    }
}