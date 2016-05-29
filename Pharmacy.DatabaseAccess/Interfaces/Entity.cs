using System;

namespace Pharmacy.DatabaseAccess.Interfaces
{
    public interface IEntity
    {
        Guid EntityId { get; set; }
    }
}
