using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            return userRepo.UserRegistration(userModel);
        }
        public UserModel GetUser(int id)
        {
            return userRepo.GetUser(id);
        }
        public string Update(UserModel userModel, int id)
        {
            return userRepo.Update(userModel, id);
        }
        public string DeleteUser(int id)
        {
            return userRepo.DeleteUser(id);
        }

        public string Login(LoginModel loginModel)
        {
            return userRepo.Login(loginModel);
        }
        public bool Email(string email)
        {
            return userRepo.Email(email);
        }
        public string Forgotpassword(string email)
        {
            return userRepo.Forgotpassword(email);
        }
        public string ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            return userRepo.ResetPassword(email, resetPasswordModel);
        }
    }
}
