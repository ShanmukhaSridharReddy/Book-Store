using BusinessLayer.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Sessions;
using System.Collections.Generic;
using System.Linq;
using System;
using ModelLayer.Model;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewBusiness reviewBusiness;
        public ReviewsController(IReviewBusiness reviewBusiness)
        {
            this.reviewBusiness = reviewBusiness;
        }
        [Authorize]
        [HttpPost("AddReviews")]
        public IActionResult AddReviews(AddReviewModel addReviewModel)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserID").Value);
                IEnumerable<ReviewResponseModel> review = reviewBusiness.AddReviews(userId, addReviewModel);
                return Ok(new { success = true, message = "review added", data = review });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "not able add the review", data = ex.Message });
            }
        }
        [HttpGet("GetReviews")]
        public IActionResult GetAllReviews(int bookId)
        {
            try
            {
                IEnumerable<ReviewResponseModel> response = reviewBusiness.GetAllReviews(bookId);
                return Ok(new { success = true, message = "reviews", data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "review not found", data = ex.Message });
            }
        }
    }
}
