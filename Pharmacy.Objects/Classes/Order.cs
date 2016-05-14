using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Objects.Enums;

namespace Pharmacy.Objects.Classes
{
    public class Order
    {
        public string PhoneNumber { get; set; }
        public OrderStatus MakeState { get; set; }
        public bool IsReady { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReadyTime { get; set; }
        public decimal Price { get; set; }
    }
}
