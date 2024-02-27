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
    public class CartRepo : ICartRepo
    {
        private readonly IConfiguration configuration;
        public CartRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string AddCart(int userId, int bookId,int quantity)
        {
            if (userId > 0 && bookId > 0)
            {
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("Addcart_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Uid", userId);
                cmd.Parameters.AddWithValue("@Pid", bookId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return "Added To Cart";
            }
            return null;
        }
        public IEnumerable<BookModel> GetAllCart(int id)
        {
            List<BookModel> list = new List<BookModel>();
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("GetAllCart_sp", sqlConnection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uid", id);
            
            sqlConnection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                BookModel cart = new BookModel();
               
                cart.id = rd.GetInt32("Pid");
                cart.title = rd["title"].ToString();
                cart.author = rd["author"].ToString(); 
                cart.image = rd["image"].ToString();
                cart.price = rd["price"].ToString();
                cart.quantity = rd["quantity"].ToString();
                cart.description = rd["description"].ToString();
                list.Add(cart);
            }
            sqlConnection.Close();
            return list;
        }
        public string UpdateCart(CartModel cartModel)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            
            SqlCommand cmd = new SqlCommand("Updatecart_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Pid", cartModel.bookId);
            cmd.Parameters.AddWithValue("@quantity", cartModel.quantity);
            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return "Updated successfully";

        }
        public string DeleteCart(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("DeleteCart_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uid", id);
            return "deleted";
        }
    }
}
