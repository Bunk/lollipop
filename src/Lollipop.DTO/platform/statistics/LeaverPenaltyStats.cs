using System;

namespace com.riotgames.platform.statistics
{
    public class LeaverPenaltyStats : VersionedObject
    {
        public bool userInformed;
        public int level;
        public int points;
        public DateTime? lastDecay;
        public DateTime? lastLevelIncrease;
    }
}