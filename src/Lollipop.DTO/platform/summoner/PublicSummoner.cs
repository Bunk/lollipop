using System;

namespace com.riotgames.platform.summoner
{
    public class PublicSummoner : VersionedObject
    {
        public int acctId;
        public int summonerId;
        public int profileIconId;

        public string name;
        public string internalName;
        public int summonerLevel;

        public int revisionId;
        public DateTime revisionDate;
    }
}