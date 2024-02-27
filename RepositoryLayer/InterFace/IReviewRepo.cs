using ModelLayer.Model;
using RepositoryLayer.Sessions;
using System.Collections.Generic;

namespace RepositoryLayer.InterFace
{
    public interface IReviewRepo
    {
        IEnumerable<ReviewResponseModel> AddReviews(int userId, AddReviewModel addReviewModel);
        IEnumerable<ReviewResponseModel> GetAllReviews(int bookId);
    }
}