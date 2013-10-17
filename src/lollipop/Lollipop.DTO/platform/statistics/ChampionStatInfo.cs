using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class ChampionStatInfo : VersionedObject
    {
        public int accountId;
        public int championId;

        public int totalGamesPlayed;

        public List<AggregatedStat> stats;

        public ChampionStatInfo()
        {
            stats = new List<AggregatedStat>();
        }
    }
}