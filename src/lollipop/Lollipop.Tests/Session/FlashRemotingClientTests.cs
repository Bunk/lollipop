using com.riotgames.platform.leagues.client.dto;
using com.riotgames.platform.statistics;
using com.riotgames.platform.summoner;
using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Session
{
    [TestFixture]
    [Category("Integration")]
    public class FlashRemotingClientTests
    {
        private static readonly IFlashRemotingClient Client;

        static FlashRemotingClientTests()
        {
            Client = new LeagueClient(new LocateServerIP(), new AuthorizationService(), new LeagueConnection());
            Client.Connect(LeagueRegion.NorthAmerica, "BunkTester", "leaguetester1").Wait();
        }

        [Test]
        public void Can_connect_to_riots_servers()
        {
            Assert.That(Client.IsConnected, Is.True);
        }

        [Test]
        public async void Can_asynchronously_invoke_multiple_commands()
        {
            var stats = await Client.Call<PlayerLifetimeStats>("playerStatsService",
                                                               "retrievePlayerStatsByAccountId",
                                                               37685970, "CURRENT");
            var data = await Client.Call<AllPublicSummonerDataDTO>("summonerService",
                                                                   "getAllPublicSummonerDataByAccount",
                                                                   37685970);
            var leagues = await Client.Call<SummonerLeaguesDTO>("leaguesServiceProxy", "getAllLeaguesForPlayer",
                                                                34060121);

            Assert.That(stats, Is.Not.Null);
            Assert.That(data, Is.Not.Null);
            Assert.That(leagues, Is.Not.Null);
        }
    }
}