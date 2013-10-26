using System.Collections.Generic;

namespace Lollipop.Session
{
    public class AuthorizationResponse
    {
        /// <summary>
        /// NOT SURE
        /// </summary>
        public int Backlog { get; set; }

        /// <summary>
        /// The request authorization token used to authorize
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The status of the login.
        /// FAILED  - When a failed attempt was made
        /// QUEUE   - When in the login queue
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The reason for a failed login
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// The queue name
        /// </summary>
        public string Champ { get; set; }

        /// <summary>
        /// The number of logins processed per tick
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// How long we should wait to try again
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Our position in the queue list
        /// </summary>
        public int Node { get; set; }

        /// <summary>
        /// The relative positions of others in the queue
        /// </summary>
        public List<AuthorizationTicker> Tickers { get; set; }

        /// <summary>
        /// The username we're trying to login as
        /// </summary>
        public string User { get; set; }

        public AuthorizationResponse()
        {
            Tickers = new List<AuthorizationTicker>();
        }
    }

    public class AuthorizationTicker
    {
        public string Champ { get; set; }

        public int Current { get; set; }

        public int Id { get; set; }

        public int Node { get; set; }
    }
}