using System;

namespace com.riotgames.platform.game
{
    public class PlatformGameLifecycleDTO : VersionedObject
    {
        public string gameName;

        public DateTime? lastModifiedDate;

        public int reconnectDelay;
        public string connectivityStateEnum;
        public object gameSpecificLoyaltyRewards;

        public GameDTO game;
        public PlayerCredentialsDto playerCredentials;
    }
}