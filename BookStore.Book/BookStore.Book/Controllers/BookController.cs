using BookStore.Book.Interface;
using BookStore.Book.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Book.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBook ibook;
        
        public BookController(IBook ibook)
        {
            this.ibook = ibook;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var result = ibook.GetBookbyId(id);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Books by id Successful", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = "Get Book by id UnSuccessful", data = result });

                }
            }catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(UpdateModel model,long id)
        {
            try
            {
                var result = ibook.updateBook(model, id);
                if (result != null)
                {
                    return Ok(new { success = true, message = " Books updated Successful", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = " Book update UnSuccessful", data = result });

                }
            }catch( Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(long id)
        {
            try
            {
                var result = ibook.Delete(id);
                if (result)
                {
                    return Ok(new { success = true, message = " Books Deleted Successful", data = result });

                }
                else
                {
                    return BadRequest(new { success = false, message = " Book Delete UnSuccessful", data = result });

                }
            }catch(Exception e)
            {
                throw e;
            }
        }
       
    }
}
