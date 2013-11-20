using Lollipop.Session;
using NUnit.Framework;

namespace Lollipop.Tests.Auth
{
    [TestFixture]
    public class AuthorizationServiceTests
    {
        [Test]
        [Category("Integration")]
        public async void Can_connect_to_auth_endpoint()
        {
            var service = new AuthorizationService();

            var response = await service.GetAuthToken(LeagueRegion.NorthAmerica, "BunkTester", "leaguetester1");

            Assert.That(response, Is.Not.Null);
        }
    }
}
