using System;

namespace com.riotgames.platform.statistics
{
    public enum TimeTrackStatType
    {
        PRACTICE_MINUTES,
        MM_BOTS_GAMES_PLAYED
    }

    public class TimeTrackedStat : VersionedObject
    {
        public string type;

        public DateTime timestamp;
    }
}