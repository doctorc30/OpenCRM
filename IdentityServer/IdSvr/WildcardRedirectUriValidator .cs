using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.Default;

namespace IdentitySolomon.IdSvr
{
    public class WildcardRedirectUriValidator : DefaultRedirectUriValidator
    {
        public override Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            return MatchUriAsync(requestedUri, client.RedirectUris);
        }

        public override Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            return MatchUriAsync(requestedUri, client.PostLogoutRedirectUris);
        }

        private Task<bool> MatchUriAsync(string requestedUri, List<string> allowedUris)

        {
            var rules = allowedUris.Select(ConvertToRegex).ToList();
            var res = rules.Any(r => Regex.IsMatch(requestedUri, r, RegexOptions.IgnoreCase));
            return Task.FromResult(res);
        }

        private const string WildcardCharacter = @"[a-zA-Z0-9\-]";

        private static string ConvertToRegex(string rule)
        {
            if (rule == null)
            {
                throw new ArgumentNullException(nameof(rule));
            }

            return Regex.Escape(rule)
                        .Replace(@"\*", WildcardCharacter + "*")
                        .Replace(@"\?", WildcardCharacter);
        }
    }
}