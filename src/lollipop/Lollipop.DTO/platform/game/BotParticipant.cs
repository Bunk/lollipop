using com.riotgames.platform.statistics;

namespace com.riotgames.platform.game
{
    public class BotParticipant : Participant
    {
        public int championId;
        public int teamId;
        public int lastSelectedSkinIndex;
        public int spell1Id;
        public int spell2Id;

        public string locale;
        public string role;

        public string summonerName;
        public string summonerInternalName;

        public int badges;
        public int pickTurn;

        public GamePickMode pickMode;
        public GameDifficulty botDifficulty;
        public string botSkillLevel;
    }
}