using ModelLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.InterFace
{
    public interface IOrderBusiness
    {
        OrderModel AddOrder(int id, OrderModel orderModel);
        IEnumerable<OrderModel> GetOrders();
        string DeleteOrder(int id);
    }
}