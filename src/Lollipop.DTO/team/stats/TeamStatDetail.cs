namespace com.riotgames.team.stats
{
    public class TeamStatDetail : VersionedObject
    {
        public TeamId teamId;
        public string teamIdString;
        public string teamStatType;
        public string teamStatTypeString;

        public int wins;
        public int losses;
        public int rating;
        public int maxRating;
        public int seedRating;
    }
}