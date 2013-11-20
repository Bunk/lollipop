using System;

namespace com.riotgames.platform.statistics
{
    public class PlayerLifetimeStats : VersionedObject
    {
        public int userId;

        public int dodgeStreak;
        public DateTime? dodgePenaltyDate;
        public DateTime? previousFirstWinOfDay;

        public PlayerStats playerStats;
        public PlayerStatSummaries playerStatSummaries;
        public LeaverPenaltyStats leaverPenaltyStats;

        public string playerStatsJson;
    }
}