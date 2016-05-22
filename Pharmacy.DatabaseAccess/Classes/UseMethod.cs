using System;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class UseMethod : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        public string UseOf { get; set; }
        public string TypeOf { get; set; }
        
        Guid IEntity.EntityId { get; set; }

        public UseMethod(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }

    }
}
