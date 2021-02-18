using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using HADotNet.Core;
using HADotNet.Core.Clients;
using HADotNet.Entities.Models;
using HADotNet.Entities.Mappers;
using HADotNet.Entities.Helpers;

namespace HADotNet.Entities
{
    public class EntityManager : IEntityManager
    {
        private readonly EntityClient _entityClient;
        private readonly StatesClient _statesClient;

        private Lazy<EntityDictionary<BinarySensor>> _binarySensors;
        private Lazy<EntityDictionary<Climate>> _climates;
        private Lazy<EntityDictionary<Light>> _lights;
        private Lazy<EntityDictionary<MediaPlayer>> _mediaPlayers;
        private Lazy<EntityDictionary<Sensor>> _sensors;
        private Lazy<EntityDictionary<Switch>> _switches;

        public EntityDictionary<BinarySensor> BinarySensors => _binarySensors.Value;
        public EntityDictionary<Climate> Climates => _climates.Value;
        public EntityDictionary<Light> Lights => _lights.Value;
        public EntityDictionary<MediaPlayer> MediaPlayers => _mediaPlayers.Value;
        public EntityDictionary<Sensor> Sensors => _sensors.Value;
        public EntityDictionary<Switch> Switches => _switches.Value;

        public EntityManager() : this(ClientFactory.GetClient<EntityClient>(), ClientFactory.GetClient<StatesClient>())
        {
        }

        public EntityManager(EntityClient entityClient, StatesClient statesClient)
        {
            _entityClient = entityClient ?? throw new ArgumentNullException(nameof(entityClient));
            _statesClient = statesClient ?? throw new ArgumentNullException(nameof(statesClient));

            _binarySensors = GetLazyEntities<BinarySensor>();
            _climates = GetLazyEntities<Climate>();
            _lights = GetLazyEntities<Light>();
            _mediaPlayers = GetLazyEntities<MediaPlayer>();
            _sensors = GetLazyEntities<Sensor>();
            _switches = GetLazyEntities<Switch>();
        }

        /// <summary>
        /// Get all entities of a specific domain
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetEntities<TEntity>() where TEntity : Entity
        {
            var domain = EntityHelper.GetDomainFromEntityType<TEntity>();
            var entityIds = await _entityClient.GetEntities(domain);

            var entityTasks = entityIds.Select(entityId => GetEntity<TEntity>(entityId)).ToArray();

            return await Task.WhenAll(entityTasks);
        }

        /// <summary>
        /// Get an entity of a specific domain
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to get</typeparam>
        /// <param name="entityId">The entityId to get from the given TEntity domain, either in [domain].[entity_id] or just [entity_id]</param>
        /// <returns></returns>
        public async Task<TEntity> GetEntity<TEntity>(string entityId) where TEntity : Entity
        {
            if (!entityId.Contains("."))
            {
                entityId = $"{EntityHelper.GetDomainFromEntityType<TEntity>()}.{entityId}";
            }

            var entityState = await _statesClient.GetState(entityId);

            var entity = EntityMapper.MapToEntity<TEntity>(entityState);

            return entity;
        }

        /// <summary>
        /// Update all entities currently registered
        /// </summary>
        /// <returns></returns>
        public async void UpdateAll()
        {
            await Task.WhenAll(
                BinarySensors.UpdateAll(),
                Climates.UpdateAll(),
                Lights.UpdateAll(),
                MediaPlayers.UpdateAll(),
                Sensors.UpdateAll(),
                Switches.UpdateAll()
            );
        }

        private Lazy<EntityDictionary<TEntity>> GetLazyEntities<TEntity>() where TEntity : Entity
        {
            return new Lazy<EntityDictionary<TEntity>>(GetEntitiesAsDictionary<TEntity>);
        }

        private EntityDictionary<TEntity> GetEntitiesAsDictionary<TEntity>() where TEntity : Entity
        {
            var entities = GetEntities<TEntity>().Result;
            var entityDictionary = new EntityDictionary<TEntity>();
            foreach (var entity in entities)
            {
                entityDictionary.Add(entity.EntityId, entity);
            }
            return entityDictionary;
        }
    }
}
