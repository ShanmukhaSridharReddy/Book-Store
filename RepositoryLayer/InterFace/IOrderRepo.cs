using ModelLayer.Model;
using System.Collections.Generic;

namespace RepositoryLayer.InterFace
{
    public interface IOrderRepo
    {
        OrderModel AddOrder(int id, OrderModel orderModel);
        IEnumerable<OrderModel> GetOrders();
        string DeleteOrder(int id);
    }
}