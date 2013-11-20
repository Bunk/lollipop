using System.Collections.Generic;

using com.riotgames.team;

namespace com.riotgames.team.dto
{
	public class PlayerDTO
	{
		public int playerId;
	    public object teamStatSummary;
		public List<CreatedTeam> createdTeams;
		public List<TeamInfo> playerTeams;
	}
}