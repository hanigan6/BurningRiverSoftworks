using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using BurningRiverSoftworks.Definitions.Auth;
using BurningRiverSoftworks.AzureServicesProvider.KeyVault;
using System;

namespace BurningRiverSoftworks.HomeAutomation.Services.Auth
{
    public class AuthService : AuthProvider.AuthProviderBase
    {
        private readonly ILogger<AuthService> _logger;

        public AuthService(ILogger<AuthService> logger)
        {
            _logger = logger;
        }

        public override async Task<TokenResponse> ConnectionAccessToken(UserClaims request, ServerCallContext context)
        {
            var secretClient = new KeyVaultFactory(new VaultCredentials { }).CreateVaultClient();
            try
            {
                var secret = await secretClient.GetSecretAsync("Temp");
                return new TokenResponse { AccessToken = secret.Value.Value, ExpiresIn = 3600, TokenType = "secret" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex.InnerException;
            }
        
        }
        public override Task<AuthenticationResponse> IsAuthenticated(UserData request, ServerCallContext context)
        {
            return base.IsAuthenticated(request, context);
        }

        public override Task<AuthorizationResponse> IsAuthorized(UserData request, ServerCallContext context)
        {
            return base.IsAuthorized(request, context);
        }
    }
}