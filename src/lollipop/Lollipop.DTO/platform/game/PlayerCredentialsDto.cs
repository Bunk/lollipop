namespace com.riotgames.platform.game
{
    public class PlayerCredentialsDto : VersionedObject
    {
        public long gameId;
        public int summonerId;
        public string summonerName;
        public int playerId; // account id
        public int championId;
        public int lastSelectedSkinIndex;

        public string serverIp;
        public int serverPort;

        public string observerServerIp;
        public int observerServerPort;

        public bool observer;

        public string encryptionKey;
        public string observerEncryptionKey;
        public string handshakeToken;
    }
}