using System.Collections.Generic;

namespace com.riotgames.platform.summoner
{
    public class TalentRow : VersionedObject
    {
        public int tltRowId;
        public int tltGroupId;
        public int index;
        public int pointsToActivate;
        public List<Talent> talents;

        public TalentRow()
        {
            talents = new List<Talent>();
        }
    }
}