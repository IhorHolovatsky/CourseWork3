using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.DatabaseAccess.Enums;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Order : IEntity
    {
        public Guid Id { get { return ((IEntity)this).EntityId; } }

        public Recipe Recipe { get; set; }

        public string PhoneNumber { get; set; }

        public int AvailabilityOfComponents { get; set; }
        public OrderState MakeState { get; set; }

        public bool IsReady
        {
            get { return MakeState == OrderState.Ready; }
        }

        public DateTime OrderDate { get; set; }
        public DateTime ReadyTime { get; set; }

        public int TotalPrice { get; set; }

        public string MedicineString
        {
            get
            {
                var medicines = from x in Recipe.Medicines
                    select x.Name;

                return string.Join(", ", medicines);
            }
        }

        Guid IEntity.EntityId { get; set; }

        public Order()
        {
        }

        public Order(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }
    }
}
