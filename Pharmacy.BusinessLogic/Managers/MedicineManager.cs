using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    class MedicineManager
    {
        private static SqlExecuteManager _sqlManager;

        static MedicineManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Medicine GetMedicineById(Guid medicineId)
        {
            var query = SqlQueryGeneration.GetMedicineById(medicineId);
            var medicines = _sqlManager.GetMedicine(query);

            if (medicines.Count == 0)
                return null;

            var ingredientQuery = SqlQueryGeneration.GetIngredientByMedicineIdId(medicineId);
            var ingredients = _sqlManager.GetIngredient(ingredientQuery);

            var medicine = medicines.First();
            medicine.Ingredients = ingredients;

            return medicine;
        }

        public static Medicine GetMedicineByName(string medicineName)
        {
            var query = SqlQueryGeneration.GetMedicineByName(medicineName);
            var medicines = _sqlManager.GetMedicine(query);

            foreach (var med in medicines)
            {
                var ingredientQuery = SqlQueryGeneration.GetIngredientByMedicineIdId(med.Id);
                var ingredients = _sqlManager.GetIngredient(ingredientQuery);

                med.Ingredients = ingredients;
            }

            return medicines.Count > 0 ? medicines.First() : null; ;
        }

        public static List<Medicine> GetAllMedicines()
        {
            var query = SqlQueryGeneration.GetAllMedicines();
            var medicines = _sqlManager.GetMedicine(query);

            foreach (var med in medicines)
            {
                var ingredientQuery = SqlQueryGeneration.GetIngredientByMedicineIdId(med.Id);
                var ingredients = _sqlManager.GetIngredient(ingredientQuery);

                med.Ingredients = ingredients;
            }

            return medicines;
        }
        #endregion

        #region INSERT

        public static void Insert(Medicine medicine)
        {
            if (medicine == null)
                throw new ArgumentException("Medicine information was not provided");

            var query = SqlQueryGeneration.InsertMedicine(medicine);

            try
            {
                _sqlManager.InsertMedicine(query, medicine.Image);
            }
            catch (SqlException e)
            {
                //ToDo: Log Exception
                throw new Exception("Failed to insert Medicine", e);
            }
        }

        #endregion

        #region UPDATE

        public static Medicine Update(Medicine medicine)
        {
            if (medicine == null)
                throw new ArgumentException("Medicine information was not provided");

            var query = SqlQueryGeneration.UpdateMedicine(medicine);

            return string.IsNullOrEmpty(query) ? medicine : _sqlManager.UpdateMedicine(query, medicine.Image);
        }

        #endregion
    }
}
