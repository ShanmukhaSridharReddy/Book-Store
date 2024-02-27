using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class CartBusiness : ICartBusiness
    {
        private readonly ICartRepo cartRepo;
        public CartBusiness(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        public string AddCart(int userId, int bookId, int quantity)
        {
            return cartRepo.AddCart(userId, bookId,quantity);
        }
        public IEnumerable<BookModel> GetAllCart(int id)
        {
            return cartRepo.GetAllCart(id);
        }
        public string UpdateCart(CartModel cartModel)
        {
            return cartRepo.UpdateCart(cartModel);
        }
        public string DeleteCart(int id)
        {
            return cartRepo.DeleteCart(id);
        }
    }
}
