using BusinessLayer.InterFace;
using MassTransit.Audit;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBusiness addressBusiness;
        public AddressController(IAddressBusiness addressBusiness)
        {
            this.addressBusiness = addressBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("AddAddress")]
        public IActionResult AddAddress(AddressModel addressModel)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            var address = addressBusiness.AddAddress(userId, addressModel);
            if (address != null)
            {
                return Ok(new ResponseModel<AddressModel> { IsSuccess = true, Message = "Address Added Successfully", Data = address });
            }
            else
            {
                return BadRequest(new ResponseModel<AddressModel> { IsSuccess = false, Message = "Not Added" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAddress")]
        public ActionResult GetAddress()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(a => a.Type =="UserID").Value);
            IEnumerable<AddressModel> lstAddress = addressBusiness.GetAddress(userId);
            if (lstAddress != null)
            {
                return Ok(new ResponseModel<IEnumerable<AddressModel>> { IsSuccess = true, Message = "Showing Address", Data = lstAddress });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "No Addresses" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateAddress")]
        public IActionResult UpdateAddress(UpdateAddressModel addressModel)
        {
            int UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
            addressModel.Uid = UserId;
            var address = addressBusiness.UpdateAddress(addressModel);
            if(address != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess = true,Message="updated",Data = "Address Updated Successfully"});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "failed", Data="Updation Failed" });
            }
        }

       
    }
}
