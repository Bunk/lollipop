namespace com.riotgames.platform.game
{
    public class PlayerChampionSelectionDTO : VersionedObject
    {
        public int championId;
        public int selectedSkinIndex;
        public int spell1Id;
        public int spell2Id;
        public string summonerInternalName;
    }
}