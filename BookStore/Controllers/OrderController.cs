using BusinessLayer.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness orderBusiness;
        public OrderController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = orderBusiness.AddOrder(userid, orderModel);
            if(result != null)
            {
                return Ok(new ResponseModel<OrderModel> { IsSuccess = true, Message = "Order Placed", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Order Not Placed"});
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllOrders")]
        public ActionResult GetAllOrder()
        {
            var result = orderBusiness.GetOrders();
            if (result != null)
            {
                return Ok(new ResponseModel<IEnumerable<OrderModel>>{IsSuccess= true, Message="Displaying Data",Data = result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "No Data" });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int id)
        {
            if (id > 0)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Deleted Successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message=" Not Deleted"});
            }
        }
    }
}
