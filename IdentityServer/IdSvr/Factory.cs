using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;

namespace IdentitySolomon.IdSvr
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure()
        {
            var factory = new IdentityServerServiceFactory();

            var scopeStore = new InMemoryScopeStore(Scopes.Get());
            var clientStore = new InMemoryClientStore(Clients.Get());

            factory.ScopeStore = new Registration<IScopeStore>(scopeStore);
            factory.ClientStore = new Registration<IClientStore>(clientStore);
            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });
            //factory.RedirectUriValidator = new Registration<IRedirectUriValidator>(typeof(WildcardRedirectUriValidator));

            return factory;
        }
    }
}
