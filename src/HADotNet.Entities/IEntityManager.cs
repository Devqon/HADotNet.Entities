using HADotNet.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HADotNet.Entities
{
    public interface IEntityManager
    {
        EntityDictionary<BinarySensor> BinarySensors { get; }
        EntityDictionary<Climate> Climates { get; }
        EntityDictionary<Light> Lights { get; }
        EntityDictionary<MediaPlayer> MediaPlayers { get; }
        EntityDictionary<Sensor> Sensors { get; }
        EntityDictionary<Switch> Switches { get; }

        Task<IEnumerable<TEntity>> GetEntities<TEntity>() where TEntity : Entity;
        Task<TEntity> GetEntity<TEntity>(string entityId) where TEntity : Entity;
    }
}