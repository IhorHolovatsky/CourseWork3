using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Objects.Interfaces
{
    internal interface IEntity
    {
        Guid EntityId { get; set; }
    }
}
