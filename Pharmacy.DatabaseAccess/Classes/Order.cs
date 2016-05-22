using System;
using System.Linq;
using Pharmacy.DatabaseAccess.Enums;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Order : IEntity
    {
        public Guid Id { get { return ((IEntity)this).EntityId; } }
        public Patient Patient { get; set; }
        public Recipe Recipe { get; set; }
        public string PhoneNumber { get; set; }
        public OrderStatus MakeState { get; set; }

        public bool IsReady
        {
            get { return MakeState == OrderStatus.Ready; }
        }

        public DateTime OrderDate { get; set; }
        public DateTime ReadyTime { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Recipe.Medicines.Sum(med => med.Price);
            }
        }

        Guid IEntity.EntityId { get; set; }

        public Order(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }
    }
}
