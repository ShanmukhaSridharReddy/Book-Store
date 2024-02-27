using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class OrderRepo : IOrderRepo
    {
        private readonly IConfiguration configuration;
        public OrderRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public OrderModel AddOrder(int id,OrderModel orderModel)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("AddOrder_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Uid", id);
            cmd.Parameters.AddWithValue("@Pid", orderModel.Pid);
            cmd.Parameters.AddWithValue("@totalprice", orderModel.totalprice);
            cmd.Parameters.AddWithValue("@quantity", orderModel.quantity);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return orderModel;
        }
        public IEnumerable<OrderModel> GetOrders()
        {
            List<OrderModel> listOrder = new List<OrderModel>();
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("GetOrder_sp", sqlConnection);
            cmd.CommandType= CommandType.StoredProcedure;

            sqlConnection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                OrderModel model = new OrderModel();
                model.userId = rd.GetInt32("Uid");
                model.Pid = rd.GetInt32("Pid");
                model.totalprice = rd.GetInt32("totalprice");
                model.quantity = rd.GetInt32("quantity");

                listOrder.Add(model);

            }
            sqlConnection.Close ();
            return listOrder;
        }
        public string DeleteOrder(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookString"));
            SqlCommand cmd = new SqlCommand("DeleteOrder_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Oid", id);
            sqlConnection.Open ();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return "Deleted";
        }
    }
}
