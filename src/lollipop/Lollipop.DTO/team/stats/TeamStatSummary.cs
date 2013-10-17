using System.Collections.Generic;

namespace com.riotgames.team.stats
{
    public class TeamStatSummary : VersionedObject
    {
        public TeamId teamId;
        public string teamIdString;

        public List<TeamStatDetail> teamStatDetails;

        public TeamStatSummary()
        {
            teamStatDetails = new List<TeamStatDetail>();
        }
    }
}