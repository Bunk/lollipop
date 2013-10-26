using System.Runtime.InteropServices;
using com.riotgames.platform.leagues;
using com.riotgames.platform.statistics;
using com.riotgames.team;
using Lollipop.Services;
using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Repositories
{
    [TestFixture]
    public class StatsServiceTests : ServiceTestsBase<IStatsService>
    {
        protected override IStatsService CreateService(ILeagueAccount connection)
        {
            return new StatsService(connection);
        }

        [Test]
        public async void Can_get_lifetime_stats_for_player()
        {
            var stats = await Service.GetLifetimeStats(37685970);

            Assert.That(stats, Is.Not.Null);
            Assert.That(stats.userId, Is.EqualTo(37685970));
            Assert.That(stats.playerStatSummaries.playerStatSummarySet.Count, Is.GreaterThan(0));
        }

        [Test]
        public async void Can_get_aggregated_stats_for_team()
        {
            var stats = await Service.GetAggregatedStats(new TeamId("TEAM-582810e0-df98-11e2-b1d8-782bcb4d1861"));

            Assert.That(stats, Is.Not.Null);
            Assert.That(stats.Count, Is.GreaterThan(0));
            Assert.That(stats[0].teamId.fullId, Is.EqualTo("TEAM-582810e0-df98-11e2-b1d8-782bcb4d1861"));
            Assert.That(stats[0].queueType, Is.EqualTo(LeagueQueue.RANKED_TEAM_5x5));
            Assert.That(stats[0].playerAggregatedStatsList.Count, Is.GreaterThan(0));
        }

        [TestCase(GameMode.CLASSIC)]
        [TestCase(GameMode.ODIN)]
        [TestCase(GameMode.ARAM)]
        public async void Can_get_aggregated_stats_for_player_by_gamemode(GameMode mode)
        {
            var stats = await Service.GetAggregatedStats(37685970, mode);

            Assert.That(stats, Is.Not.Null);
            Assert.That(stats.key.gameMode, Is.EqualTo(mode));
            Assert.That(stats.key.userId, Is.EqualTo(37685970));

            if (mode == GameMode.CLASSIC) // ARAM and DOMINION don't send any stats back yet
                Assert.That(stats.lifetimeStatistics.Count, Is.GreaterThan(0));
        }

        [TestCase(GameMode.CLASSIC)]
        [TestCase(GameMode.ODIN)]
        [TestCase(GameMode.ARAM)]
        public async void Can_get_most_played_champs_by_gamemode(GameMode mode)
        {
            var stats = await Service.GetMostPlayedChamps(37685970, mode);

            Assert.That(stats, Is.Not.Null);

            // ARAM and DOMINION don't send any stats back yet
            foreach (var stat in stats)
            {
                Assert.That(stat.accountId, Is.EqualTo(37685970));
                Assert.That(stat.championId, Is.GreaterThan(-1));
                Assert.That(stat.totalGamesPlayed, Is.GreaterThan(0));
                Assert.That(stat.stats.Count, Is.GreaterThan(0));
            }
        }

        [TestCase(37685970)]
        [TestCase(32801644)]
        [TestCase(31829019)]
        public async void Can_get_recent_games(int accountId)
        {
            var stats = await Service.GetRecentGames(accountId);

            Assert.That(stats, Is.Not.Null);
            Assert.That(stats.userId, Is.EqualTo(accountId));
            Assert.That(stats.gameStatistics.Count, Is.GreaterThan(0));
        }

        [Test]
        public async void Can_get_end_of_game_stats()
        {
            var stats = await Service.GetGameStats(new TeamId("TEAM-7362d288-7d5c-45be-a3d6-4e39bd925679"), 1010408992);

            Assert.That(stats, Is.Not.Null);
        }
    }
}