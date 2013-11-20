namespace com.riotgames.team
{
	public class TeamId : VersionedObject
	{
		//"TEAM-11111111-2222-3333-4444-555555555555"
		public string fullId;

        public TeamId() { }

	    public TeamId(string id)
	    {
	        fullId = id;
	    }
	}
}