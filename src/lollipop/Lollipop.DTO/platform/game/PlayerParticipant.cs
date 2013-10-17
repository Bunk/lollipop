using com.riotgames.platform.statistics;

namespace com.riotgames.platform.game
{
    public class PlayerParticipant : VersionedObject
    {
        public long accountId;
        public long summonerId;
        public string partnerId;
        public int profileIconId;
        public int lastSelectedSkinIndex;
        public int index;

        public bool teamOwner;
        public bool rankedTeamGuest;
        public bool minor;
        public bool clientInSynch; // sp!

        public string summonerName;
        public string summonerInternalName;
        public string originalPlatformId; // ie, NA
        public string originalAccountNumber;

        public int pickTurn;
        public int queueRating;
        public long timeAddedToQueue;

        public string locale;
        public string selectedRole; // ?
        public string selectedPosition;

        public GameDifficulty botDifficulty;
        public int badges; // probably bit enum
    }
}