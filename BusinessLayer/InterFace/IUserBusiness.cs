using ModelLayer.Model;

namespace BusinessLayer.InterFace
{
    public interface IUserBusiness
    {
        UserModel UserRegistration(UserModel userModel);
        UserModel GetUser(int id);
        string Update(UserModel userModel, int id);
        string DeleteUser(int id);
        string Login(LoginModel loginModel);
        bool Email(string email);
        string Forgotpassword(string email);
        string ResetPassword(string email, ResetPasswordModel resetPasswordModel);

    }
}