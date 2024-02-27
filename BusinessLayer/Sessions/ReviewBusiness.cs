using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using RepositoryLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class ReviewBusiness : IReviewBusiness
    {
        private readonly IReviewRepo reviewRepo;
        public ReviewBusiness(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }
        public IEnumerable<ReviewResponseModel> AddReviews(int userId, AddReviewModel addReviewModel)
        {
            return reviewRepo.AddReviews(userId, addReviewModel);
        }
        public IEnumerable<ReviewResponseModel> GetAllReviews(int bookId)
        {
            return reviewRepo.GetAllReviews(bookId);
        }
    }
}
