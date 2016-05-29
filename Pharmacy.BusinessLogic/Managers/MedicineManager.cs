using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public class MedicineManager
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
            return FillObjectData(query).FirstOrDefault();
        }

        public static Medicine GetMedicineByName(string medicineName)
        {
            var query = SqlQueryGeneration.GetMedicineByName(medicineName);
            return FillObjectData(query).FirstOrDefault();
        }

        public static List<Medicine> GetMedicinesByRecipeId(Guid recipeId)
        {
            var query = SqlQueryGeneration.GetMedicineByRecipeId(recipeId);
            return FillObjectData(query);
        } 

        public static List<Medicine> GetAllMedicines()
        {
            var query = SqlQueryGeneration.GetAllMedicines();
            return FillObjectData(query); ;
        }
        #endregion

        #region INSERT

        public static Guid Insert(Medicine medicine)
        {
            if (medicine == null)
                throw new ArgumentException("Medicine information was not provided");

            var query = SqlQueryGeneration.InsertMedicine(medicine);

            try
            {
                return _sqlManager.InsertMedicine(query, medicine.Image);

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


            var returnMedicine = _sqlManager.UpdateMedicine(query, medicine.Image);
            returnMedicine.Ingredients = IngredientManager.GetByMedicineId(returnMedicine.Id);

            return returnMedicine;
        }

        #endregion

        #region DELETE

        public static bool Delete(Medicine medicine)
        {
            if (medicine == null || medicine.Id == Guid.Empty)
                throw new ArgumentException("MedicineId information was not provided");

            var query = SqlQueryGeneration.DeleteMedicine(medicine);
            
            return _sqlManager.DeleteMedicine(query);
        }

        #endregion


        private static List<Medicine> FillObjectData(string query)
        {
            var medicines = _sqlManager.GetMedicine(query);
            
            foreach (var medicine in medicines)
            {
                medicine.Ingredients = IngredientManager.GetByMedicineId(medicine.Id);
            }

            return medicines;
        }
    }
}
