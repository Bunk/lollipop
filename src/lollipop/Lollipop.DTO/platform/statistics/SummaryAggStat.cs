namespace com.riotgames.platform.statistics
{
    public class SummaryAggStat : VersionedObject
    {
        public SummaryAggStatType statType { get; set; }

        public double value { get; set; }

        public double count { get; set; }
    }
}