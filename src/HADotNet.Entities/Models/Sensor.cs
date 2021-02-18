using HADotNet.Entities.Constants;

namespace HADotNet.Entities.Models
{
    public class Sensor : Entity
    {
        public override string Domain => DomainConstants.Sensor;

        public Sensor(string entityId) : base(entityId)
        {
        }
    }
}
