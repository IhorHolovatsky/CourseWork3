using System.Collections.Generic;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Recipe
    {
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public List<Medicine> Medicines { get; set; } 
        public string Diagnoz { get; set; }
    }
}
