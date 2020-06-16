using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using BurningRiverSoftworks.Definitions.Location;
using BurningRiverSoftworks.Definitions.Auth;

namespace GrpcClientPOC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new LocationProvider.LocationProviderClient(grpcChannel);

            var locations = new List<CurrentLocation>
            {
                new CurrentLocation{TimeStamp = DateTime.Now.ToString(), LocationData = "Number One", Id = "1"},
                new CurrentLocation{TimeStamp = DateTime.Now.AddSeconds(10).ToString(), LocationData = "Number Two", Id = "2"},
                new CurrentLocation{TimeStamp = DateTime.Now.AddDays(10).ToString(), LocationData = "Number 3", Id = "3"},

            };
            
            using var call = client.StreamCurrentLocation();
            foreach(var location in locations)
            {
                //await Task.Delay(10000);                
                await call.RequestStream.WriteAsync(new CurrentLocation { Id = location.Id, LocationData = location.LocationData, TimeStamp = location.TimeStamp});
            }
            await call.RequestStream.CompleteAsync();
            var response = await call;
            Console.WriteLine($"Response: {response.Id}");

            var authClient = new AuthProvider.AuthProviderClient(grpcChannel);

            var authResponse = await authClient.ConnectionAccessTokenAsync(new UserClaims{GrantType = "Blah", Scope ="scopedBlah", ClientId = "Test Proj", ClientSecret = "12345"});

            Console.WriteLine($"Auth Response: {authResponse.AccessToken}");
            
           
        }
    }
}
