using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            return bookRepo.AddBook(bookModel);
        }
        public IEnumerable<BookModel> GetBooks()
        {
            return bookRepo.GetBooks();
        }
        public BookModel GetById(int id)
        {
            return bookRepo.GetById(id);
        }
        public IEnumerable<BookModel> GetByAuthor(string author)
        {
            return bookRepo.GetByAuthor(author);
        }
        public string UpdateBook(BookModel bookModel)
        {
            return bookRepo.UpdateBook(bookModel);
        }
        public string DeleteBook(int id)
        {
            return bookRepo.DeleteBook(id);
        }
    }
}
