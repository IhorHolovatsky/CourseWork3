using System;
using System.Collections.Generic;
using Pharmacy.DatabaseAccess.Interfaces;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Ingredient : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId;; } }

        private SqlExecuteManager _sqlManager;
        public string Name { get; set; }

        public int Count { get; set; }

        public int ReservedCount { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Ingredient()
        {
            
        }

        public Ingredient(Guid entityId)
        {
            ((IEntity)this).EntityId = entityId;
        }
        
        public List<Medicine> GetMedicines()
        {
            var query = SqlQueryGeneration.GetAllMedicinesByIngredientId(((IEntity) this).EntityId);
            var medicines = _sqlManager.GetMedicine(query);

            return medicines;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
