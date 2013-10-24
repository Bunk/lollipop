using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class SummaryAggStats : VersionedObject
    {
        public string statsJson;

        public List<SummaryAggStat> stats;

        public SummaryAggStats()
        {
            stats = new List<SummaryAggStat>();
        }
    }
}