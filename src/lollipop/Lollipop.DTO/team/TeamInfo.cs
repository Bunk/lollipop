namespace com.riotgames.team
{
    public class TeamInfo : VersionedObject
    {
        public TeamId teamId;
        public string name;
        public string tag;
        public string memberStatus;
        public string memberStatusString;
        public float secondUntilEligibleForDeletion;
    }
}