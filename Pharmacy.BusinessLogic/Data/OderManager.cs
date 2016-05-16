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
    class OrderManager
    {
        private static SqlExecuteManager _sqlManager;

        static OrderManager()
        {
            _sqlManager = new SqlExecuteManager();
        }

        //#region GET
        //public static Order GetOrderById(Guid orderId)
        //{
        //    var query = SqlQueryGeneration.GetOrderById(orderId);
        //    var orders = _sqlManager.GetOrder(query);

        //    if (orders.Count == 0)
        //        return null;

        //    var ingredientQuery = SqlQueryGeneration.GetIngredientByOrderIdId(orderId);
        //    var ingredients = _sqlManager.GetIngredient(ingredientQuery);

        //    var order = orders.First();
        //    order.Ingredients = ingredients;

        //    return order;
        //}

        //public static Order GetOrderByName(string orderName)
        //{
        //    var query = SqlQueryGeneration.GetOrderByName(orderName);
        //    var orders = _sqlManager.GetOrder(query);

        //    foreach (var med in orders)
        //    {
        //        var ingredientQuery = SqlQueryGeneration.GetIngredientByOrderIdId(med.Id);
        //        var ingredients = _sqlManager.GetIngredient(ingredientQuery);

        //        med.Ingredients = ingredients;
        //    }

        //    return orders.Count > 0 ? orders.First() : null; ;
        //}

        //public static List<Order> GetAllOrders()
        //{
        //    var query = SqlQueryGeneration.GetAllOrders();
        //    var orders = _sqlManager.GetOrder(query);

        //    foreach (var med in orders)
        //    {
        //        var ingredientQuery = SqlQueryGeneration.GetIngredientByOrderIdId(med.Id);
        //        var ingredients = _sqlManager.GetIngredient(ingredientQuery);

        //        med.Ingredients = ingredients;
        //    }

        //    return orders;
        //}
        //#endregion

        //#region INSERT

        //public static void Insert(Order order)
        //{
        //    if (order == null)
        //        throw new ArgumentException("Order information was not provided");

        //    var query = SqlQueryGeneration.InsertOrder(order);

        //    try
        //    {
        //        _sqlManager.InsertOrder(query, order.Image);
        //    }
        //    catch (SqlException e)
        //    {
        //        //ToDo: Log Exception
        //        throw new Exception("Failed to insert Order", e);
        //    }
        //}

        //#endregion

        //#region UPDATE

        //public static Order Update(Order order)
        //{
        //    if (order == null)
        //        throw new ArgumentException("Order information was not provided");

        //    var query = SqlQueryGeneration.UpdateOrder(order);

        //    return string.IsNullOrEmpty(query) ? order : _sqlManager.UpdateOrder(query, order.Image);
        //}

        //#endregion
    }
}
