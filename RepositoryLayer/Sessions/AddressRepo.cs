using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class AddressRepo : IAddressRepo
    {
        private readonly IConfiguration _configuration;
        public AddressRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AddressModel AddAddress(int userId, AddressModel addressModel)
        {
            if(userId > 0)
            {

            AddressModel model = new AddressModel();
            SqlConnection sqlConnection = new SqlConnection(this._configuration["ConnectionStrings:BookStore"]);
            SqlCommand cmd = new SqlCommand("AddAddress_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Uid", userId);
            cmd.Parameters.AddWithValue("@fullName", addressModel.fullName);
            cmd.Parameters.AddWithValue("@fullAddress", addressModel.fullAddress);
            cmd.Parameters.AddWithValue("@city", addressModel.city);
            cmd.Parameters.AddWithValue("@state", addressModel.state);
            cmd.Parameters.AddWithValue("@phoneNo", addressModel.phoneNo);//long.Parse(userModel.phoneNo
            cmd.Parameters.AddWithValue("@Type", addressModel.Type);
            cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return model;
            }
            else
            {

            return null;
            }

        }
        public IEnumerable<AddressModel> GetAddress(int userId)
        {
            List<AddressModel> lstAddress = new List<AddressModel>();
            SqlConnection sqlConnection = new SqlConnection(this._configuration["ConnectionStrings:BookStore"]);
            SqlCommand cmd = new SqlCommand("GetAddress_sp", sqlConnection);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uid",userId);

            sqlConnection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                AddressModel addressModel = new AddressModel();
                addressModel.fullName = rd["fullName"].ToString();
                addressModel.fullAddress = rd["fullAddress"].ToString();
                addressModel.city = rd["city"].ToString();
                addressModel.state = rd["state"].ToString();
                addressModel.phoneNo = rd["phoneNo"].ToString();
                addressModel.Type = rd.GetInt32("Type");
                lstAddress.Add(addressModel);
            }
            sqlConnection.Close();
            return lstAddress;
        }      
        public string UpdateAddress(UpdateAddressModel updateAddressModel)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionStrings:BookStore"]);
            SqlCommand cmd = new SqlCommand("UpdateAddress_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uid", updateAddressModel.Uid);
            cmd.Parameters.AddWithValue("@Aid",updateAddressModel.Aid);
            cmd.Parameters.AddWithValue("@fullAddress", updateAddressModel.fullAddress);
            cmd.Parameters.AddWithValue("@city", updateAddressModel.city);
            cmd.Parameters.AddWithValue("@state", updateAddressModel.state);
            cmd.Parameters.AddWithValue("@UpdatedAt",DateTime.Now);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return "Updated";
        }


        
    }
}
