using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public static class IngredientManager
    {
        private static SqlExecuteManager _sqlManager;

        static IngredientManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Ingredient GetById(Guid ingredientId)
        {
            var query = SqlQueryGeneration.GetIngredientById(ingredientId);
            var ingredients = _sqlManager.GetIngredient(query);

            return ingredients.Count > 0 ? ingredients.First() : null;
        }


        public static List<Ingredient> GetByName(string ingredientName)
        {
            var query = SqlQueryGeneration.GetIngredientByName(ingredientName);
            var ingredients = _sqlManager.GetIngredient(query);

            return ingredients;
        }

        public static Ingredient GetByMedicineId(Guid medicineId)
        {
            var query = SqlQueryGeneration.GetIngredientByMedicineIdId(medicineId);
            var ingredients = _sqlManager.GetIngredient(query);

            return ingredients.Count > 0 ? ingredients.First() : null;
        }

        public static List<Ingredient> GetAll()
        {
            var query = SqlQueryGeneration.GetAllIngredients();

            var ingredients = _sqlManager.GetIngredient(query);

            return ingredients;
        } 

        #endregion


        public static void Insert(Ingredient ingredient)
        {
            if (ingredient == null)
                throw new ArgumentException("Medicine information was not provided");

            var query = SqlQueryGeneration.InsertIngredient(ingredient);

            try
            {
                _sqlManager.InsertIngredient(query);
            }
            catch (SqlException e)
            {
                //ToDo: Log Exception
                throw new Exception("Failed to insert Medicine", e);
            }
        }
    }
}
