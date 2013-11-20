namespace com.riotgames
{
    /// <summary>
    /// Not a part of RIOT's object structure AFAIK
    /// </summary>
    public abstract class VersionedObject
    {
        /// <summary>
        /// Unknown
        /// </summary>
        public int dataVersion { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public object futureData { get; set; }
    }
}