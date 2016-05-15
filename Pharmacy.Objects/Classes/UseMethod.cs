using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Interfaces;

namespace Pharmacy.Objects.Classes
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
