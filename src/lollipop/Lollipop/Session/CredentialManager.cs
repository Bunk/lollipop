using System.Globalization;
using com.riotgames.platform.login;

namespace Lollipop.Session
{
    public class CredentialManager
    {
        private const string ClientVersion = "3.12.13_09_24_17_41";
        private const string ClientDomain = "lolclient.lol.riotgames.com";

        public AuthenticationCredentials Generate(string serverIp, string username, string password, string token)
        {
            return new AuthenticationCredentials
            {
                clientVersion = ClientVersion,
                domain = ClientDomain,
                locale = CultureInfo.CurrentCulture.ToString().Replace("-", "_"),
                ipAddress = serverIp ?? "127.0.0.1",
                username = username.ToLowerInvariant(),
                password = password,
                authToken = token
            };
        }
    }
}