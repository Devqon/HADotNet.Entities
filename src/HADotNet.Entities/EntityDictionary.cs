using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using HADotNet.Entities.Models;

namespace HADotNet.Entities
{
    public class EntityDictionary<TEntity> : Dictionary<string, TEntity> where TEntity : Entity
    {
        public async Task UpdateAll()
        {
            await Task.WhenAll(Values.Select(entity => entity.Update()));
        }
    }
}
