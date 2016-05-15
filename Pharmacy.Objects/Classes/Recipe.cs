using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Objects.Classes
{
    public class Recipe
    {
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public List<Medicine> Medicines { get; set; } 
        public string Diagnoz { get; set; }
    }
}
