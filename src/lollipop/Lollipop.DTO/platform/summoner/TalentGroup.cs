using System.Collections.Generic;

namespace com.riotgames.platform.summoner
{
    public class TalentGroup : VersionedObject
    {
        public int tltGroupId;
        public int index;
        public string name;
        public List<TalentRow> talentRows;

        public TalentGroup()
        {
            talentRows = new List<TalentRow>();
        }
    }
}