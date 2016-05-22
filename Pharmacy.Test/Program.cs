using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ingredientTest = new IngredientTest();

            while (true)
            {
                var inputStr = Console.ReadLine();
                if (inputStr == "exit" || inputStr == null) { break; }

                var names = inputStr.Split(',').ToList();

                foreach (var ingredientName in names)
                {
                    ingredientTest.Add(ingredientName);
                }
            }
        }
    }
}
