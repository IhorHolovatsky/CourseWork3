using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Objects.Classes
{
    public class Patient
    {
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public string SecondaryName { get; set; }
         public DateTime DateOfBirth { get; set; }
    }
}
