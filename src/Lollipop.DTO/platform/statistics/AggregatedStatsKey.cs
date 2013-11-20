namespace com.riotgames.platform.statistics
{
    public class AggregatedStatsKey : VersionedObject
    {
        public int userId;
        public int season;
        public GameMode gameMode;
        public string gameModeString;
    }
}