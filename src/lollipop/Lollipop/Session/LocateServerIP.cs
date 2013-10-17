using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lollipop.Session
{
    public class LocateServerIP : ILocateServerIP
    {
        public async Task<string> Locate(LeagueRegion region)
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync(region.IpLookupUri);
                var response = JsonConvert.DeserializeObject<IPResponse>(content);

                return response.ip_address;
            }
        }
    }
}