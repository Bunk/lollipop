namespace Lollipop.Session
{
    public interface ICredentials
    {
        string username { get; set; }

        string password { get; set; }

        string clientVersion { get; set; }

        string domain { get; set; }

        string locale { get; set; }
        
        string ServerUrl { get; set; }

        string ipAddress { get; set; }
    }
}