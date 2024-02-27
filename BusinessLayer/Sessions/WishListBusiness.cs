using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.InterFace;

namespace BusinessLayer.Sessions
{
    public class WishListBusiness : IWishListBusiness
    {
        private readonly IWishListRepo wishListRepo;

        public WishListBusiness(IWishListRepo wishListRepo)
        {
            this.wishListRepo = wishListRepo;
        }
        public string AddWishList(int userId, int bookId)
        {
            return wishListRepo.AddWishList(userId, bookId);
        }
        public string RemoveWishList(int userId, int bookId)
        {
            return wishListRepo.RemoveWishList(userId, bookId);
        }
    }
}
