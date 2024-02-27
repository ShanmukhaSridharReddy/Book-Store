using BusinessLayer.InterFace;
using GreenPipes.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBusiness cartBusiness;
        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddCart(int bookid, int quantity)
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = cartBusiness.AddCart(userid, bookid, quantity);
            if (result != null)
            {
                return Ok(new ResponseModel<string>{IsSuccess= true,Message="Added Successfully", Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = true, Message = "Not Added" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllCart")]
        public ActionResult GetAllCart()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = cartBusiness.GetAllCart(userId);
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<BookModel>> { IsSuccess= true, Message="Data Found",Data=result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false,Message = "Data Not Found" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(CartModel cartModel)
        {
            var result = cartBusiness.UpdateCart(cartModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Updated Succesfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Update Failed" });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteCart")]
        public IActionResult DeleteCart()
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = cartBusiness.DeleteCart(userid);
            if(result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message ="Deleted Successfull",Data = result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Delete Failed"});
            }
        }
    }
}
