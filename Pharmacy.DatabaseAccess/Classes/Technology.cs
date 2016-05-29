using System;
using System.Collections.Generic;
using Pharmacy.DatabaseAccess.Interfaces;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.DatabaseAccess.Classes
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

        public List<Medicine> GetMedicines()
        {
            var query = SqlQueryGeneration.GetAllMedicinesByIngredientId(this.Id);
            return new SqlExecuteManager().GetMedicine(query);
        }
    }
}
