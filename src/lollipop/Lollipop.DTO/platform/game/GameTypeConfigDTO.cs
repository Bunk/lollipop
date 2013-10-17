namespace com.riotgames.platform.game
{
    public class GameTypeConfigDTO : VersionedObject
    {
        public int id;
        public string name;

        public bool allowTrades;
        public bool exclusivePick;
        public bool duplicatePick;

        public string pickMode;
        public int maxAllowableBans;
        public int banTimerDuration;
        public int mainPickTimerDuration;
        public int postPickTimerDuration;
    }
}
