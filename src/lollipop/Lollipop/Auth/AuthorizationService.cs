using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Lollipop.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lollipop.Auth
{
    public class AuthorizationService : IAuthorize
    {
        public async Task<string> GetAuthToken(LeagueRegion region, string username, string password)
        {
            var authResponse = await TryLogin(region, username, password);
            if (!string.IsNullOrWhiteSpace(authResponse.Token))
                return authResponse.Token;

            if (authResponse.Status == "FAILED")
                throw new InvalidOperationException("Error logging in: " + authResponse.Reason);

            var currentTicker = authResponse.Tickers.FirstOrDefault(ticker => ticker.Node == authResponse.Node);
            return await ProcessTicker(region, authResponse, currentTicker);
        }

        private async Task<AuthorizationResponse> TryLogin(LeagueRegion region, string username, string password)
        {
            var payload = string.Format("user={0},password={1}", username, password);
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"payload", payload}
            });

            using (var client = new HttpClient())
            {
                var uri = new Uri(region.QueueUri, "/login-queue/rest/queue/authenticate");
                var response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException("Unsuccessful response from authenticate endpoint: " +
                                                        response.StatusCode);

                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthorizationResponse>(data);
            }
        }

        private async Task<string> ProcessTicker(LeagueRegion region, AuthorizationResponse response,
                                                 AuthorizationTicker ticker)
        {
            var position = ticker.Id - ticker.Current;

            Debug.WriteLine("In login queue (#" + position + " in line)");
            if (position <= response.Rate)
                return await ProcessAuthToken(region, response);

            // Sleep until the queue updates
            await Task.Delay(TimeSpan.FromMilliseconds(response.Rate));

            using (var client = new HttpClient())
            {
                var uri = new Uri(region.QueueUri, "/login-queue/rest/queue/ticker/" + response.Champ);
                var data = await client.GetStringAsync(uri);
                var parsed = JObject.Parse(data);

                JToken nextPosition;
                if (!parsed.TryGetValue(response.Node.ToString(), out nextPosition))
                    return await ProcessTicker(region, response, ticker);

                var hex = nextPosition.Value<string>();
                var next = Convert.ToInt32(hex, 16); // convert from Hex (base-16)

                ticker.Current = next;

                return await ProcessTicker(region, response, ticker);
            }
        }

        private async Task<string> ProcessAuthToken(LeagueRegion region, AuthorizationResponse response,
                                                    int retryCount = 0)
        {
            if (!string.IsNullOrWhiteSpace(response.Token))
                return response.Token;

            if (retryCount > 5)
                throw new TimeoutException("Retry limit exceeded.  Try again later.");

            // Sleep for a small amount of time
            await Task.Delay(TimeSpan.FromMilliseconds(response.Delay/10));

            Debug.WriteLine("Retrieving auth token for user: {0}, attempt: {1}", response.User, retryCount);

            using (var client = new HttpClient())
            {
                try
                {
                    var uri = new Uri(region.QueueUri, "/login-queue/rest/queue/authToken/" + response.User);
                    var data = await client.GetStringAsync(uri);
                    var parsed = JsonConvert.DeserializeObject<AuthorizationResponse>(data);
                    return await ProcessAuthToken(region, parsed, retryCount);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Could not parse response, retrying.");
                }

                return await ProcessAuthToken(region, response, retryCount + 1);
            }
        }
    }
}