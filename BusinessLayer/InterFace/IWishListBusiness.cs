namespace BusinessLayer.InterFace
{
    public interface IWishListBusiness
    {
        string AddWishList(int userId, int bookId);
        string RemoveWishList(int userId, int bookId);
    }
}