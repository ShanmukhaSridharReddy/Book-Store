namespace RepositoryLayer.InterFace
{
    public interface IWishListRepo
    {
        string AddWishList(int userId, int bookId);
        string RemoveWishList(int userId, int bookId);
    }
}