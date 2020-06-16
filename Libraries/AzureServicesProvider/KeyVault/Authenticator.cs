using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace BurningRiverSoftworks.AzureServicesProvider.KeyVault
{
    public class Authenticator
    {
        private readonly ILogger<Authenticator> _logger;
        private const string uriScope = "https://vault.azure.net/.default";
        private readonly VaultCredentials _vaultCredentials;

        public Authenticator(VaultCredentials vaultCredentials)
        {
            _vaultCredentials = vaultCredentials;
        }

        public async Task<string> AuthenticationCallback(string authority, string resource, string scope)
        {
            _logger.LogInformation($"Authenticating with Azure for {uriScope}");
            var credential = ConfidentialClientApplicationBuilder.Create("")
                .WithClientSecret("")
                .WithTenantId("")
                .Build();
            var result = await credential.AcquireTokenForClient(new string[] { uriScope }).ExecuteAsync();
            return result.AccessToken;
        }
    }
}
