using System.Configuration;

namespace elcoin.Admin
{
    public class Constants
    {
        public static readonly string Domain = ConfigurationManager.AppSettings["Domain"];
        public static readonly string DomainUrl = ConfigurationManager.AppSettings["DomainUrl"];
        public static readonly string SSODomain = ConfigurationManager.AppSettings["ssoDomain"];
        public static readonly string DomainShort = ConfigurationManager.AppSettings["DomainShort"];
        public static readonly string TokenEndpoint = "https://" + Domain + "/connect/token";
        public static readonly string UserInfoEndpoint = "https://" + Domain + "/connect/userinfo";
    }
}