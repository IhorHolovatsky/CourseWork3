using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.BusinessLogic.Managers;
using Pharmacy.DatabaseAccess.Classes;

namespace Pharmacy.Test
{
    public class IngredientTest
    {
        public void Add(string name)
        {
            var random = new Random();

            var ingredient = new Ingredient(Guid.NewGuid())
            {
                Name = name,
                Count = random.Next(0,100),
                ReservedCount = random.Next(0,100)
            };

            IngredientManager.Insert(ingredient);
        }
    }
}
