using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BurningRiverSoftworks.Definitions.Location;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace BurningRiverSoftworks.HomeAutomation.Services.Location
{
    public class LocationService : LocationProvider.LocationProviderBase
    {
        private readonly ILogger<LocationService> _logger;
        public LocationService(ILogger<LocationService> logger)
        {
            _logger = logger;
        }
        public override async Task<LocationValidator> StreamCurrentLocation(IAsyncStreamReader<CurrentLocation> requestStream, ServerCallContext context)
        {
            var locationList = new List<string>();
            while(await requestStream.MoveNext())
            {
                var current = requestStream.Current;
                locationList.Add(current.LocationData);
            }
            return new LocationValidator { Id = string.Join(",", locationList), IsValid = true };
        }

    }
}