namespace com.riotgames.platform.catalog
{
    public class ItemEffect : VersionedObject
    {
        public int effectId;
        public int itemId;
        public int itemEffectId;
        public double value;

        public Effect effect;
    }
}