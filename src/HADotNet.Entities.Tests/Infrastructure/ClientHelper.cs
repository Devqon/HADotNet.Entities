using System;
using HADotNet.Core;

namespace HADotNet.Entities.Tests.Infrastructure
{
    public static class ClientHelper
    {
        public static void InitializeClientFactory()
        {
            var instance = new Uri(Environment.GetEnvironmentVariable("HADotNet:Tests:Instance"));
            var apiKey = Environment.GetEnvironmentVariable("HADotNet:Tests:ApiKey");

            ClientFactory.Initialize(instance, apiKey);
        }
    }
}
