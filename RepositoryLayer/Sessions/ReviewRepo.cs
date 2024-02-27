using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly IConfiguration configuration;
        public ReviewRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IEnumerable<ReviewResponseModel> AddReviews(int userId, AddReviewModel addReviewModel)
        {
            using (SqlConnection sqlConnection = new SqlConnection(this.configuration["ConnectionStrings:BookStore"]))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("add_review", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@review", addReviewModel.review);
                cmd.Parameters.AddWithValue("@star", addReviewModel.star);
                cmd.Parameters.AddWithValue("@bookId", addReviewModel.bookId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return GetAllReviews(addReviewModel.bookId);
            }
        }
        public IEnumerable<ReviewResponseModel> GetAllReviews(int bookId)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionStrings:BookStore"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("get_review", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Pid", bookId);
            SqlDataReader rd = cmd.ExecuteReader();
            List<ReviewResponseModel> reviews = new List<ReviewResponseModel>();
            while (rd.Read())
            {
                ReviewResponseModel review = new ReviewResponseModel();
                review.name = rd.GetString("name");
                review.star = rd.GetInt32("rating");
                review.review = rd.GetString("review");
                review.bookId = rd.GetInt32("Pid");
                reviews.Add(review);
            }
            return reviews;
        }
    }
}
