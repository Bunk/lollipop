namespace com.riotgames.platform.summoner
{
    public class Talent : VersionedObject
    {
        public int tltId;
        public int talentGroupId;
        public int talentRowId;
        public int index;
        public string name;
        public string level1Desc;
        public string level2Desc;
        public string level3Desc;
        public string level4Desc;
        public string level5Desc;
        public int minLevel;
        public int maxRank;
        public int minTier;
        public int gameCode;
        public int? prereqTalentGameCode;
    }
}