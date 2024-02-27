using Microsoft.Extensions.Configuration;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class WishListRepo : IWishListRepo
    {
        private readonly IConfiguration _configuration;
        public WishListRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string AddWishList(int userId,int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this._configuration["ConnectionStrings:BookStore"]);
            SqlCommand sqlCommand = new SqlCommand("AddWishlist_sp", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Uid",userId);
            sqlCommand.Parameters.AddWithValue("Pid", bookId);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return "added";
        }
        public string RemoveWishList(int userId,int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this._configuration["ConnectionStrings:BookStore"]);
            SqlCommand sqlCommand = new SqlCommand("DeleteWishList_sp", sqlConnection);
            sqlCommand.CommandType=CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Uid", userId);
            sqlCommand.Parameters.AddWithValue("Pid",bookId);

            sqlConnection.Open() ;
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return "Removed from WishList";
        }
    }
}
