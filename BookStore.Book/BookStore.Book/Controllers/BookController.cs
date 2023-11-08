using BookStore.Book.Interface;
using BookStore.Book.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBook ibook;
        
        public BookController(IBook ibook)
        {
            this.ibook = ibook;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddBook")]

        public IActionResult Addbook(BookModel book)
        {
            try
            {
                var result = ibook.AddBook(book);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Book Adding Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book Adding UnSuccessful", data = result });

                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]

        public IActionResult GetBook()
        {
            try
            {
                var result = ibook.GetBooks();
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Books Successful", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = "Get Book  UnSuccessful", data = result });

                }
            }catch(Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int id)
        {
            var result=ibook.GetBookbyId(id);
            if (result != null)
            {
                return Ok(new { success = true, message = "Get Books by id Successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "Get Book by id UnSuccessful", data = result });

            }
        }

        [HttpPost]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(UpdateModel model,long id)
        {
            var result=ibook.updateBook(model, id);
            if (result != null)
            {
                return Ok(new { success = true, message = " Books updated Successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = " Book update UnSuccessful", data = result });

            }
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(long id)
        {
           var result=ibook.Delete(id);
            if (result)
            {
                return Ok(new { success = true, message = " Books Deleted Successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = " Book Delete UnSuccessful", data = result });

            }
        }
       
    }
}
