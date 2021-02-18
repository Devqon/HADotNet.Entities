using System;
using HADotNet.Entities.Models;

namespace HADotNet.Entities
{
    internal static class EntityFactory
    {
        public static TEntity CreateEntity<TEntity>(string entityId) where TEntity : Entity
        {
            return (TEntity)Activator.CreateInstance(typeof(TEntity), entityId);
        }
    }
}
