using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class SummaryAggStats : VersionedObject
    {
        public List<SummaryAggStat> stats { get; set; }

        public SummaryAggStats()
        {
            stats = new List<SummaryAggStat>();
        }
    }
}