using BusinessLayer.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBusiness wishListBusiness;
        public WishListController(IWishListBusiness wishListBusiness)
        {
            this.wishListBusiness = wishListBusiness;
        }
        [Authorize]
        [HttpPost]
        [Route("AddToWishList")]
        public IActionResult AddWishlist(int bookId)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = wishListBusiness.AddWishList(id, bookId);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Not Added");
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("RemoveWishList")]
        public IActionResult RemoveWishlist(int bookId)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(a => a.Type == "UserID").Value);
            var result = wishListBusiness.RemoveWishList(id, bookId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Not Removed");
            }
        }
    }
}
