using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentitySolomon.IdSvr
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Solomon24",
                    ClientId = "solomon24",
                    Flow = Flows.Implicit,
                    AllowRememberConsent = true,
                    AllowAccessToAllScopes = true,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.Address
                    },
                    RequireConsent = false,
                    ClientUri = IdentityServerConstants.Solomon24Url,

                    RedirectUris = new List<string>
                    {
                        IdentityServerConstants.Solomon24Url
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        IdentityServerConstants.Solomon24Url
                    },
                    LogoutUri = IdentityServerConstants.Solomon24Url + "Account/SignoutCleanup",
                    LogoutSessionRequired = true,
                },
                new Client
                {
                    ClientName = "SolomonElcoin",
                    ClientId = "solomon.elcoin",
                    Flow = Flows.Implicit,
                    AllowRememberConsent = true,
                    AllowAccessToAllScopes = true,
                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.Address
                    },
                    RequireConsent = false,
                    ClientUri = IdentityServerConstants.SolomonElcoinUrl,

                    RedirectUris = new List<string>
                    {
                        IdentityServerConstants.SolomonElcoinUrl
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        IdentityServerConstants.SolomonElcoinUrl
                    },
                    LogoutUri = IdentityServerConstants.SolomonElcoinUrl + "Account/SignoutCleanup",
                    LogoutSessionRequired = true,
                }
            };
        }
    }
}