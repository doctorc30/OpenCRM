using System.Configuration;

namespace IdentitySolomon
{
    public static class IdentityServerConstants
    {
        public static readonly string Solomon24Url = ConfigurationManager.AppSettings["Solomon24Domain"];
        public static readonly string SolomonElcoinUrl = ConfigurationManager.AppSettings["SolomonElcoinDomain"];
    }
}