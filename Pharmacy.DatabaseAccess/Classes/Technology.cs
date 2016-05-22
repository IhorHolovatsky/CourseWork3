using System;
using System.Collections.Generic;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Technology : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }
        public string PreparationMethod { get; set; }

        public List<Medicine> Medicines { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Technology(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }
    }
}
