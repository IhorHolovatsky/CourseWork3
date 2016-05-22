using System;

namespace Pharmacy.DatabaseAccess.Interfaces
{
    internal interface IEntity
    {
        Guid EntityId { get; set; }
    }
}
