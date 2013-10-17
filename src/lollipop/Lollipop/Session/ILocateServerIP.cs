using System.Threading.Tasks;

namespace Lollipop.Session
{
    public interface ILocateServerIP
    {
        Task<string> Locate(LeagueRegion region);
    }

    public class IPResponse
    {
        public string ip_address { get; set; }
    }
}