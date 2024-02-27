using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepo repo;
        public OrderBusiness(IOrderRepo orderRepo)
        {
            repo = orderRepo;
        }
        public OrderModel AddOrder(int id, OrderModel orderModel)
        {
            return repo.AddOrder(id,orderModel);
        }
        public IEnumerable<OrderModel> GetOrders()
        {
            return repo.GetOrders();
        }
        public string DeleteOrder(int id)
        {
            return repo.DeleteOrder(id);
        }
    }
}
