using com.riotgames.platform.statistics;

namespace com.riotgames.platform.gameclient.domain
{
	public class RawStat : VersionedObject
	{
		public RawStatType statType;
		public int value;
	}
}
