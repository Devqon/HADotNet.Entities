using HADotNet.Entities.Constants;

namespace HADotNet.Entities.Models
{
    /// <summary>
    /// Represents a binary sensor entity
    /// </summary>
    public class BinarySensor : Entity
    {
        public override string Domain => DomainConstants.BinarySensor;

        /// <summary>
        /// Creates a binary sensor entity
        /// </summary>
        public BinarySensor(string entityId) : base(entityId)
        {
        }
    }
}
