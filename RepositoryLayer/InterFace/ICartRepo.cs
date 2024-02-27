using ModelLayer.Model;
using System.Collections.Generic;

namespace RepositoryLayer.InterFace
{
    public interface ICartRepo
    {
        string AddCart(int userId, int bookId, int quantity);
        IEnumerable<BookModel> GetAllCart(int id);
        string UpdateCart(CartModel cartModel);
        string DeleteCart(int id);
    }
}