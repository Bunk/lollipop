namespace com.riotgames.platform.game
{
    public class PlayerChampionSelectionDTO : VersionedObject
    {
        /// <summary>
        /// The selected champion id, or 0 if not selected
        /// </summary>
        public int championId;
        public int selectedSkinIndex;
        public int spell1Id;
        public int spell2Id;
        public string summonerInternalName;
    }
}