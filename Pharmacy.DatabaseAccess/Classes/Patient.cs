using System;
using System.Collections.Generic;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Patient : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondaryName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Order> Orders { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Patient()
        {
            
        }

        public Patient(Guid id)
        {
            ((IEntity) this).EntityId = id;
        }
    }
}
