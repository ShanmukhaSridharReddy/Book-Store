using BusinessLayer.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness bookBusiness;
        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
        }

        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookModel book)
        {
            BookModel isAdded = bookBusiness.AddBook(book);
            if(isAdded != null)
            {
                return Ok(new ResponseModel<BookModel> { IsSuccess= true,Message="Book Added" ,Data=isAdded});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Not Added" });
            }
        }
        [HttpGet]
        [Route("BooksList")]
        public ActionResult BooksList()
        {
            IEnumerable<BookModel> book = bookBusiness.GetBooks();
            if (book != null)
            {
                return Ok(new ResponseModel<IEnumerable<BookModel>> { IsSuccess = true, Message = "User Data", Data = book });
            }
            return BadRequest(new ResponseModel<string> { IsSuccess = true, Message = "No User Found", Data = "no data" });

            
        }

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetById(int id )
        {
            try
            {
                BookModel book = bookBusiness.GetById(id);
                return Ok(new ResponseModel<BookModel> { IsSuccess = true, Message = "book.title", Data = book });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "book not found", Data = ex.Message });
            }

        }
        [HttpGet]
        [Route("AuthorBooksList")]
        public IActionResult AuthorBooksList(string author)
        {
            //List<BookModel> bookModels = bookBusiness.GetByAuthor(author);
            List<BookModel> booksList = (List<BookModel>)bookBusiness.GetByAuthor(author);
            if(booksList != null)
            {
                return Ok(new ResponseModel<List<BookModel>> { IsSuccess = true, Message="Books Found", Data=booksList });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false,Message = "Not Found"});
            }


        }
        [HttpPut]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(BookModel book)
        {
            var result = bookBusiness.UpdateBook(book);
            if(result != null)
            {
                return Ok(new ResponseModel<string> { IsSuccess= true, Message="Book Updated Successfull" , Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess=false, Message="Updation Failed"});
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            var result = bookBusiness.DeleteBook(id);
            if(result != null )
            {
                return Ok(new ResponseModel<string> { IsSuccess=true, Message="Book deleted Successfully", Data=result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Deletion Failed" });
            }
        }
    }
}
