using BusinessLayer.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult UserRegistration(UserModel userModel)
        {
            var isAdded = userBusiness.UserRegistration(userModel);
            if(isAdded != null)
            {
                return Ok(new ResponseModel<UserModel> { IsSuccess = true, Message = "Registration Successful", Data = isAdded });
            }
            else
            {
                return BadRequest(new ResponseModel<UserModel> { IsSuccess = false, Message = "Registration Failed" });
            }
        }
       
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int id)
        {
            UserModel user = userBusiness.GetUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User Not found");
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(UserModel userModel)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);

            var result = userBusiness.Update(userModel,id);

            if (result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess=true, Message="Updated Successfull", Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = true, Message = "Not Updated" });
            }
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var result = userBusiness.DeleteUser(id);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "user deleted successfully." });

            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "user deletion failed." });

            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = userBusiness.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message="Login Successful", Data= result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess= false, Message ="Invalid User",Data ="Failed To Login"});
            }
        }
        [HttpGet]
        [Route("EmailExixts")]
        public IActionResult ValidEmail(string email)
        {
            var result = userBusiness.Email(email);
            if (result)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Valid User", Data = "Email Exists" });

            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = " Invalid User ", Data = "Email Not Exists" });
            }
        }
        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var IsExists = userBusiness.Email(email);
            if (IsExists)
            {
                var result = userBusiness.Forgotpassword(email);
                if(result != null)
                {
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message ="Mail Sent Successfully",Data= result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess=false, Message="Mail Sending Failed"});
                }
            }
            else 
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = true, Message = "Email Doesn't Exists" });
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            string email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            if (email != null)
            {
                var result = userBusiness.ResetPassword(email, model);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Password Reset Successfull." });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Password Reset Failed." });
                }
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Unable to retrieve user email." });
            }
        }

    }
}
