using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.SqlHelpers;
using Pharmacy.Objects.Classes;

namespace Pharmacy.BusinessLogic.Data
{
    public static class IngredientManager
    {
        private static SqlExecuteManager _sqlManager;

        static IngredientManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Ingredient GetIngredientById(int ingredientId)
        {
            var query = SqlQueryGeneration.GetIngredientById(ingredientId);
            var ingredients = _sqlManager.GetIngredient(query);

            return ingredients.Count > 0 ? ingredients.First() : null;
        }
        #endregion
    }
}
