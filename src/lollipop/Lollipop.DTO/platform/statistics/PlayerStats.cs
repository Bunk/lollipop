using System;
using System.Collections.Generic;

namespace com.riotgames.platform.statistics
{
    public class PlayerStats : VersionedObject
    {
        public int promoGamesPlayed;

        public DateTime? promoGamesPlayedLastUpdated;

        public List<TimeTrackedStat> timeTrackedStats;
    }
}