using Microsoft.Extensions.Configuration;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Sessions
{
    public class BookRepo : IBookRepo
    {
        private readonly IConfiguration configuration;
        public BookRepo(IConfiguration config)
        {
            configuration = config;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            if (bookModel != null)
            {

                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("AddProduct_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@title", bookModel.title);
                cmd.Parameters.AddWithValue("@author", bookModel.author);
                cmd.Parameters.AddWithValue("@image", bookModel.image);
                cmd.Parameters.AddWithValue("@quantity", bookModel.quantity);
                cmd.Parameters.AddWithValue("@price", bookModel.price);
                cmd.Parameters.AddWithValue("@description", bookModel.description);


                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                return bookModel;
            }
            else
            {
                return null;
            }

        }
        public IEnumerable<BookModel> GetBooks()
        {
            List<BookModel> books = new List<BookModel>();
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("GetAllProducts_sp",sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            sqlConnection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                BookModel book = new BookModel();

                book.id = rd.GetInt32("Pid");
                book.title = rd["title"].ToString();
                book.author = rd["author"].ToString();
                book.image = rd["image"].ToString();
                book.quantity = rd["quantity"].ToString();
                book.price = rd["price"].ToString();
                book.description = rd["description"].ToString();
                books.Add(book);

            }
            sqlConnection.Close();

            return books;
        }

        public BookModel GetById(int id)
        {
            if( id != null)
            {
                BookModel book = new BookModel();
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("GetProductById_sp", sqlConnection);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pid", id);
                sqlConnection.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    book.id=rd.GetInt32("Pid");
                    book.title = rd["title"].ToString();
                    book.author = rd["author"].ToString();
                    book.image = rd["image"].ToString();
                    book.quantity = rd["quantity"].ToString();
                    book.price = rd["price"].ToString();
                    book.description = rd["description"].ToString();
                }
                sqlConnection.Close();
                return book;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<BookModel> GetByAuthor(string author)
        {
            if(author !=  null) 
            { 
                List<BookModel> bookList = new List<BookModel>();
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("GetProductByAuthor_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@author", author);

                sqlConnection.Open();
                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    BookModel book = new BookModel();
                    book.id = rd.GetInt32("Pid");
                    book.title = rd["title"].ToString();
                    book.author = rd["author"].ToString() ;
                    book.image = rd["image"].ToString();
                    book.quantity = rd["quantity"].ToString();
                    book.price = rd["price"].ToString();
                    book.description = rd["description"].ToString();

                    bookList.Add(book);
                }
                sqlConnection.Close();

                return bookList;
            }
            
            else
            {
                return null;
            }
        }

        public string UpdateBook(BookModel bookModel)
        {
            if(bookModel != null)
            {
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
                SqlCommand cmd = new SqlCommand("UpdateProduct_sp", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Pid", bookModel.id);
                cmd.Parameters.AddWithValue("@title", bookModel.title);
                cmd.Parameters.AddWithValue("@author", bookModel.author);
                cmd.Parameters.AddWithValue("@image", bookModel.image);
                cmd.Parameters.AddWithValue("@quantity", bookModel.quantity);
                cmd.Parameters.AddWithValue("@price", bookModel.price);
                cmd.Parameters.AddWithValue("@description", bookModel.description);

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();

                return "Book Updated Successfully";
            }
            else
            {
                return null;
            }
            
        }
        public string DeleteBook(int id)
        {
            if(id >= 1)
            {
            SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("BookStore"));
            SqlCommand cmd = new SqlCommand("DeleteProduct_sp", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Pid", id);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return "Book Deleted";
            }
            else
            {
                return null;
            }
        }

    }
}
