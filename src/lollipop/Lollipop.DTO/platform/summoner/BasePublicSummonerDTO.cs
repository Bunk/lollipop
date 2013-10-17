namespace com.riotgames.platform.summoner
{
    public enum LeagueTier
    {
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        DIAMOND,
        CHALLENGER
    }

	public class BasePublicSummonerDTO
	{
        public int acctId;
        public int sumId;
        public int profileIconId;

        public string name;
		public string internalName;
	    public LeagueTier previousSeasonHighestTier;
		public LeagueTier seasonTwoTier;
	}
}

