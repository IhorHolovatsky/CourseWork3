using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Comparers
{
    public class EntityComparer : EqualityComparer<IEntity>
    {
        public override bool Equals(IEntity x, IEntity y)
        {
            return x.EntityId == y.EntityId;
        }

        public override int GetHashCode(IEntity obj)
        {
            return obj == null ? 0 : obj.EntityId.GetHashCode();
        }
    }
}
