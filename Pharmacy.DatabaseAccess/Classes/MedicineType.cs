using System;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class MedicineType : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        public string TypeOf { get; set; }
        
        Guid IEntity.EntityId { get; set; }

        public MedicineType()
        {
        }

        public MedicineType(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }

    }
}
