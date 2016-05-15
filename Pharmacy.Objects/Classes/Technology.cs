using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Interfaces;

namespace Pharmacy.Objects.Classes
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
