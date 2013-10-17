using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests
{
    [TestFixture]
    public class LeagueSessionTests
    {
        [Test]
        public async void Uses_an_identity_map()
        {
            var factory = new LeagueSessionFactory();
            using (var session = factory.Create())
            {
//                var thatsbunk = await league.Summoners.ByName("ThatsBunk");
//                league.Summoners.GetName(21342);
//                league.Summoners.GetNamesBySummonerId(new[] {1234, 512341, 4221});
//                league.Summoners.PublicDataByAccount(123415);

            }
        }
    }
}