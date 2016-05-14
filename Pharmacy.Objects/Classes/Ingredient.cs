using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Interfaces;

namespace Pharmacy.Objects.Classes
{
    public class Ingredient : IEntity
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public int ReservedCount { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Ingredient(Guid entityId)
        {
            ((IEntity)this).EntityId = entityId;
        }
    }
}
