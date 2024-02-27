using ModelLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.InterFace
{
    public interface IBookBusiness
    {
        BookModel AddBook(BookModel bookModel);
        IEnumerable<BookModel> GetBooks();
        BookModel GetById(int id);
        IEnumerable<BookModel> GetByAuthor(string author);
        string UpdateBook(BookModel bookModel);
        string DeleteBook(int id);
    }
}