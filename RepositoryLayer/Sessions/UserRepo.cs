
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration config;
        public UserRepo(IConfiguration config)
        {
            this.config = config;
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            string encryptPwd = EncodePassword(userModel.Password);

            if (userModel != null)
            {
                SqlConnection sqlConnection = new SqlConnection(config["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddUser_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", userModel.UserName);
                cmd.Parameters.AddWithValue("@email", userModel.Email);
                cmd.Parameters.AddWithValue("@password", encryptPwd);
                cmd.Parameters.AddWithValue("@phoneNo", long.Parse(userModel.phoneNo));
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return userModel;
            }
            else
            {
                return null;
            }
            
        }
        public UserModel GetUser(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("GetUserById_sp", sqlConnection);
            UserModel userModel = new UserModel();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Uid", id);
            sqlConnection.Open ();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                userModel.UserName = rd["UserName"].ToString();
                userModel.Email = rd["email"].ToString();
                userModel.Password = rd["Password"].ToString();
                userModel.phoneNo = rd["phoneNo"].ToString();
                cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);

            }
            sqlConnection.Close ();
            return userModel;
        }
       
        public string Update(UserModel userModel,int id)
        {
            string encryptpwd = EncodePassword(userModel.Password);
            if(userModel != null)
            {
                SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("UpdateUser_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Uid", id);
                cmd.Parameters.AddWithValue("@Username", userModel.UserName);
                cmd.Parameters.AddWithValue("@email", userModel.Email);
                cmd.Parameters.AddWithValue("@password", encryptpwd);
                cmd.Parameters.AddWithValue("@phoneNo", long.Parse(userModel.phoneNo));
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return "User is Updated";
            }
            else 
            {
                return null; 
            }
        }
        public string DeleteUser(int id)
        {
            if(id >= 1)
            { 
            SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("DeleteUser_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Uid", id);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return "User Deleted ";
            }
            else
            {
                 return null;
            }
        }

        public string Login(LoginModel loginModel)
        {
            int id = 0;
            string encryptPwd = EncodePassword(loginModel.Password);
            if (loginModel != null)
            {
                UserModel user = new UserModel();

                SqlConnection sqlConnection = new SqlConnection(config["ConnectionStrings:BookStore"]);
                SqlCommand cmd = new SqlCommand("LoginUser_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", loginModel.Email);
                cmd.Parameters.AddWithValue("@password", encryptPwd);

                sqlConnection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        id = Convert.ToInt32(rd["Uid"]);
                        user.UserName = rd["UserName"].ToString();
                        user.Email = rd["email"].ToString();
                        user.Password = rd["Password"].ToString();
                        user.phoneNo = rd["phoneNo"].ToString();

                    }
                    var token = GenerateToken(id, user.Email);
                    if (id == 0)
                    {
                        return null;
                    }
                    return token;

                }
                else { return null; }
            }
            else
            {
                return null;
            }

        }


        public bool Email(string email)
        {
            int emailCount = 0;
            if (email != null)
            {
                using (SqlConnection con = new SqlConnection(this.config["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("validEmail_sp", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int emailExistsCount))
                    {
                        emailCount = emailExistsCount;
                    }
                    con.Close();
                    if (emailCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        public string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        private string GenerateToken(int userId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserID",userId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string Forgotpassword(string email)
        {
            int id = 0;
            if (email != null)
            {
                UserModel user = new UserModel();

                SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("forgotpassword_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                sqlConnection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    
                        while (rd.Read())
                        {
                            id = Convert.ToInt32(rd["Uid"]);
                            user.UserName = rd["UserName"].ToString();
                            user.Email = rd["email"].ToString();
                            user.Password = rd["Password"].ToString();
                            user.phoneNo = rd["phoneNo"].ToString();

                        }
                        var token = GenerateToken(id, user.Email);
                        return token;
                }
                else
                {
                    return null; 
                }
            }
            else
            {
                return null;
            }
        }

        public string ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            var newpwd = EncodePassword(resetPasswordModel.NewPassword);
            var confpwd = EncodePassword(resetPasswordModel.NewPassword);

            if (email != null && newpwd != null && confpwd != null)
            {
                if (newpwd.Equals(confpwd))
                {
                    SqlConnection sqlConnection = new SqlConnection(config.GetConnectionString("BookStore"));
                    SqlCommand cmd = new SqlCommand("ResetPassword_sp", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", newpwd);
                    cmd.Parameters.AddWithValue("@UpdatedAt",DateTime.Now);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    return "Password Reset Successfully................!";
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }


    }
}
