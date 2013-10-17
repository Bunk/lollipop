using System;

namespace com.riotgames.platform.summoner
{
    public class Summoner : VersionedObject
    {
        public int acctId;
        public int sumId;
        public int profileIconId;
        public int revisionId;

        public string name;
        public string internalName;

        public bool tutorialFlag;
        public bool advancedTutorialFlag;
        public bool displayEloQuestionaire;
        public bool helpFlag;
        public bool nameChangeFlag;

        public DateTime lastGameDate;
        public DateTime revisionDate;

        public string seasonOneTier;

        //Unknown type
        //public List<object> socialNetworkUserIds;
    }
}