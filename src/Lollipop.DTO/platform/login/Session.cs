using com.riotgames.platform.account;

namespace com.riotgames.platform.login
{
    public class Session
    {
        public string token { get; set; }

        public string password { get; set; }

        public AccountSummary accountSummary { get; set; }
    }
}