namespace com.riotgames.platform.statistics
{
    public class RawStatDTO : VersionedObject
    {
        public string statTypeName;
        public double value;
    }

    public class RawStat : VersionedObject
    {
        public RawStatType statType;
        public int value;
    }
}