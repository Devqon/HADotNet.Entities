using System.Threading.Tasks;
using NUnit.Framework;
using HADotNet.Core;
using HADotNet.Core.Clients;
using HADotNet.Entities.Models;
using HADotNet.Entities.Tests.Infrastructure;

namespace HADotNet.Entities.Tests.Models
{
    public class MediaPlayerTest
    {
        private const string MY_MEDIA_PLAYER = "my_media_player";

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
        public async Task Pause_ShouldPauseMediaPlayer()
        {
            var mediaPlayer = await _entitiesService.GetEntity<MediaPlayer>(MY_MEDIA_PLAYER);

            await mediaPlayer.Pause();

            Assert.AreEqual("paused", mediaPlayer.State);
        }
    }
}
