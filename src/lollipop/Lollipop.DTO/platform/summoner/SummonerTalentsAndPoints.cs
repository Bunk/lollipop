using System;
using System.Collections.Generic;

namespace com.riotgames.platform.summoner
{
    public class SummonerTalentsAndPoints : VersionedObject
    {
        public int summonerId;
        public int talentPoints;

        public DateTime? modifyDate;
        public DateTime? createDate;

        public List<SummonerAssociatedTalent> summonerAssociatedTalents;

        public SummonerTalentsAndPoints()
        {
            summonerAssociatedTalents = new List<SummonerAssociatedTalent>();
        }
    }
}