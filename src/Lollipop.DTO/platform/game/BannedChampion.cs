namespace com.riotgames.platform.game
{
    public class BannedChampion : VersionedObject
    {
        public int championId;
        public int teamId; // 100 or 200
        public int pickTurn;
    }
}