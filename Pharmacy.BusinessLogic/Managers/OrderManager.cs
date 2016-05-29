using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Pharmacy.DatabaseAccess.Classes;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.BusinessLogic.Managers
{
    public class OrderManager
    {
        private static SqlExecuteManager _sqlManager;

        static OrderManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        #region GET
        public static Order GetById(Guid orderId)
        {
            var query = SqlQueryGeneration.GetOrderById(orderId);
            var orders = _sqlManager.GetOrder(query);

            FillObjectData(ref orders);

            return orders.FirstOrDefault();
        }

        public static Order GetByPatientPhone(string patientPhone)
        {
            var query = SqlQueryGeneration.GetOrderByPathientPhone(patientPhone);
            var orders = _sqlManager.GetOrder(query);

            FillObjectData(ref orders);

            return orders.FirstOrDefault();
        }

        public static List<Order> GetAll()
        {
            var query = SqlQueryGeneration.GetAllOrders();
            var orders = _sqlManager.GetOrder(query);

            FillObjectData(ref orders);

            return orders;
        }
        #endregion

        #region INSERT

        public static void Insert(Order order)
        {
            if (order == null)
                throw new ArgumentException("Order information was not provided");

            var query = SqlQueryGeneration.InsertOrder(order);

            try
            {
                _sqlManager.InsertOrder(query);
            }
            catch (SqlException e)
            {
                //ToDo: Log Exception
                throw new Exception("Failed to insert Order", e);
            }
        }

        #endregion
        
        private static void FillObjectData(ref List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Recipe = RecipeManager.GetByOrderId(order.Id); ;
            }
        }
    }
}
