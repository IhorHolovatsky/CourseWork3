using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Interfaces;

namespace Pharmacy.Objects.Classes
{
    public class Medicine : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        public string Name { get; set; }

        public string Description { get; set; }

        public Image Image { get; set; }

        public string ImageUrl { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public UseMethod UseMethod { get; set; }

        public List<Technology> CreaTechnologies { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Medicine(Guid medicineId)
        {
            ((IEntity)this).EntityId = medicineId;
        }
    }
}
