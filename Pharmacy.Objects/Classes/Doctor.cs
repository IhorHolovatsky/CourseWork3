﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Interfaces;

namespace Pharmacy.Objects.Classes
{
    public class Doctor : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SecondaryName { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Doctor(Guid entityId)
        {
            ((IEntity) this).EntityId = entityId;
        }
    }
}
