using System.Threading.Tasks;
using NUnit.Framework;
using HADotNet.Core;
using HADotNet.Core.Clients;
using HADotNet.Entities.Models;
using HADotNet.Entities.Tests.Infrastructure;

namespace HADotNet.Entities.Tests.Models
{
    public class ClimateTest
    {
        private const string MY_CLIMATE = "my_climate";

        private EntitiesService _entitiesService;

        [SetUp]
        public void Setup()
        {
            ClientHelper.InitializeClientFactory();

            var statesClient = ClientFactory.GetClient<StatesClient>();
            var entityClient = ClientFactory.GetClient<EntityClient>();

            _entitiesService = new EntitiesService(entityClient, statesClient);
        }

        [Test, Explicit]
        public async Task SetHvacMode_ShouldSetHvacMode()
        {
            var climate = await _entitiesService.GetEntity<Climate>(MY_CLIMATE);

            await climate.SetHvacMode("auto");

            Assert.AreEqual("auto", climate.HvacAction);
        }
    }
}
