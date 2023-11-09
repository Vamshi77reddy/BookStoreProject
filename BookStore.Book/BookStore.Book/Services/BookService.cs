using BookStore.Book.Context;
using BookStore.Book.Entity;
using BookStore.Book.Interface;
using BookStore.Book.Migrations;
using BookStore.Book.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookStore.Book.Services
{
    public class BookService : IBook
    {
        private readonly BookContext context;
        private readonly IConfiguration configuration;

        public BookService(BookContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BookEntity AddBook(BookModel book)
        {
            try
            {
                BookEntity bookEntity = new BookEntity();

                bookEntity.BookName = book.BookName;
                bookEntity.Description = book.Description;
                bookEntity.Author = book.Author;
                bookEntity.QuantityAvailable = book.QuantityAvailable;
                bookEntity.ActualPrice = book.ActualPrice;
                bookEntity.DiscountedPrice = book.DiscountedPrice;
                context.Add(bookEntity);
                context.SaveChanges();
                return bookEntity;
            }catch(Exception ex)
            {
               throw new Exception("Error adding the book to the database.", ex);

            }

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<BookEntity> GetBooks()
        {
            try
            {
                var result = context.Book.ToList();
                if (result.Count > 0)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error fetching the list of books from the database.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BookEntity GetBookbyId(int id)
        {
            try
            {
                var bookEntity = context.Book.First(x => x.BookId == id);
                if (bookEntity != null)
                {
                    return bookEntity;
                }
                else
                {
                    return null;
                }
            }catch (Exception ex)
            {
                throw new Exception("Error fetching a book by ID from the database.", ex);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BookEntity updateBook(UpdateModel update,long id)
        {
            try
            {
                var bookEntity = context.Book.First(x => x.BookId == id);
                if (bookEntity != null)
                {

                    bookEntity.BookName = update.BookName;
                    bookEntity.Description = update.Description;
                    bookEntity.Author = update.Author;
                    bookEntity.QuantityAvailable = update.QuantityAvailable;
                    bookEntity.ActualPrice = update.ActualPrice;
                    bookEntity.DiscountedPrice = update.DiscountedPrice;
                    context.SaveChanges();
                    return bookEntity;

                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                throw new Exception("Error updating the book in the database.", ex);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(long id)
        {
            try
            {
                var result = context.Book.First(x => x.BookId == id);
                if (result != null)
                {
                    context.Book.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error deleting the book from the database.", ex);

            }
        }
    }
}
