using System;
using System.Collections.Generic;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Recipe : IEntity
    {
        public Guid Id { get { return ((IEntity)this).EntityId; } }

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }

        public List<Medicine> Medicines { get; set; }

        public string Diagnoz { get; set; }
        Guid IEntity.EntityId { get; set; }

        public Recipe()
        {
            
        }

        public Recipe(Guid id)
        {
            ((IEntity)this).EntityId = id;
        }
    }
}
