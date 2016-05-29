using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public class RecipeManager
    {
        private static SqlExecuteManager _sqlManager;

        static RecipeManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static List<Recipe> GetByPatientId(Guid patientId)
        {
            var query = SqlQueryGeneration.GetRecipeByPatientId(patientId);
            return FillObjectData(query);
        }

        public static Recipe GetByOrderId(Guid orderId)
        {
            var query = SqlQueryGeneration.GetRecipeByOrderId(orderId);
            return FillObjectData(query).FirstOrDefault();
        }

        #endregion

        #region INSERT

        public static Guid Insert(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentException("Recipe information was not provided");

            var query = SqlQueryGeneration.InsertRecipe(recipe);
            return _sqlManager.InsertRecipe(query);
        }

        #endregion

        private static List<Recipe> FillObjectData(string query)
        {
            var recipes = _sqlManager.GetRecipe(query);

            foreach (var recipe in recipes)
            {
                recipe.Patient = PatientManager.GetByRecipeId(recipe.Id);
                recipe.Doctor = DoctorManager.GetDoctorByRecipeId(recipe.Id);
                recipe.Medicines = MedicineManager.GetMedicinesByRecipeId(recipe.Id);
            }
        
            return recipes;
        }
    }
}
