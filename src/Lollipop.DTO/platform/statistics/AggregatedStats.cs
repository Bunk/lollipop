using System;
using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class AggregatedStats : VersionedObject
    {
        public AggregatedStatsKey key;

        public DateTime? modifyDate;
        
        public string aggregatedStatsJson;

        public List<AggregatedStat> lifetimeStatistics;
    }
}