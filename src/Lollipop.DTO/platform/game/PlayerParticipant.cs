using com.riotgames.platform.statistics;

namespace com.riotgames.platform.game
{
    public class PlayerParticipant : Participant
    {
        public long accountId;
        public long summonerId;
        public long teamParticipantId;
        public string partnerId; // can be an empty string... yay!
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
        public string locale;

        public int pickTurn;
        public int queueRating;
        public long timeAddedToQueue;

        public string selectedRole; // ?
        public string selectedPosition; // ?

        public GameDifficulty botDifficulty;
        public GamePickMode pickMode;
        public int badges; // probably bit enum
    }
}