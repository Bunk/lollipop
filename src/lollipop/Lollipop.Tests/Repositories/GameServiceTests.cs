using System.Linq;
using com.riotgames.platform.game;
using Lollipop.Services;
using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class GameServiceTests : ServiceTestsBase<IGameService>
    {
        protected override IGameService CreateService(ILeagueConnection connection)
        {
            return new GameService(connection);
        }

        [Test]
        public async void Can_pull_active_game_information()
        {
            // Find some public games first
            var games = await Service.FeaturedGames();
            var summoner = (from g in games.gameList
                            from p in g.participants
                            where !p.bot
                            select p).FirstOrDefault();

            if (summoner == null)
                Assert.Inconclusive("The featured games service is unavailable.");

            // Then use one of the names in the list to perform the test
            // note: The account you use must have signed in to set itself up before
            // this will work correctly.
            var game = await Service.GetActiveGameFor(summoner.summonerName);

            Assert.That(game, Is.Not.Null);
            Assert.That(game.game.gameState, Is.EqualTo(GameState.IN_PROGRESS));
        }
    }
}