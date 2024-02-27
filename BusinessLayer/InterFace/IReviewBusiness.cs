using ModelLayer.Model;
using RepositoryLayer.Sessions;
using System.Collections.Generic;

namespace BusinessLayer.InterFace
{
    public interface IReviewBusiness
    {
        IEnumerable<ReviewResponseModel> AddReviews(int userId, AddReviewModel addReviewModel);
        IEnumerable<ReviewResponseModel> GetAllReviews(int bookId);
    }
}