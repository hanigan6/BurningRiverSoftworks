using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace BurningRiverSoftworks.AzureServicesProvider.KeyVault
{
    public class KeyVaultFactory
    {
        private readonly VaultCredentials _vaultCredentials;

        public KeyVaultFactory(VaultCredentials vaultCredentials)
        {
            _vaultCredentials = vaultCredentials;
        }

        public SecretClient CreateVaultClient()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<IVaultClient>();
            //string auth = new Authenticator(_vaultCredentials);

            //above is old pkg - testing to see what I like better
            return new SecretClient(
               new Uri("https://burning-keyvault.vault.azure.net/"), 
               new DefaultAzureCredential(),
               new SecretClientOptions
               {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
               });
           
        }
    }

    public interface IVaultClient : IDisposable
    {
        Task<string> GetSecretAsync(string keyName);
    }
    public class VaultClient : IVaultClient, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecretAsync(string keyName)
        {
            throw new NotImplementedException();
        }
    }
}
