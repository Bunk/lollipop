namespace com.riotgames.platform.account
{
    public class AccountSummary : VersionedObject
    {
        public int accountId;

        public bool admin;
        public bool hasBetaAccess;
        public bool partnerMode;
        public bool needsPasswordReset;

        public string username;
        public string summonerName;
        public string summonerInternalName;
    }
}
