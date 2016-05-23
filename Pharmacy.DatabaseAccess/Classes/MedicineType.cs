using System;
using System.Runtime.Serialization;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    [DataContract]
    public class MedicineType : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        [DataMember]
        public string TypeOf { get; set; }
        
        [DataMember]
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
