using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Upskilling.DTOs;
using Upskilling.Entities;
using Upskilling.Models;

namespace Upskilling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        UpskillingDbContext Db = new UpskillingDbContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = Db.Books.Include(b => b.Category).AsNoTracking().ToList();

            var BookCatDTO = new List<BookWithCategory>();
            foreach(var book in books)
            {
                BookCatDTO.Add(new BookWithCategory() 
                {
                    BookId = book.BookId,
                    BookName = book.Name,
                    BookAuthor = book.Auther,
                    BookDescription = book.Descrition,
                    BookPrice = book.Price,
                    BooksInStock = book.Stock,
                    CategoryName = book.Category.Name
                });
            }
            if (books is not null)
            {
                return Ok(new { Books = BookCatDTO, Message = "Data Exist" });
            }
            else
            {
                return NotFound(new { ErrorMessage = "Sorry! No Categories exist!" });
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            var book = Db.Books.Include(b => b.Category).AsNoTracking().FirstOrDefault(b => b.BookId == id);
            if(book is not null)
            {
                var BookCategoryDTO = new BookWithCategory()
                {
                    BookId = book.BookId,
                    BookName = book.Name,
                    BookAuthor = book.Auther,
                    BookDescription = book.Descrition,
                    BookPrice = book.Price,
                    BooksInStock = book.Stock,
                    CategoryName = book.Category.Name
                };

                return Ok(new { book = BookCategoryDTO, Message = "Data Exist" });
            }
            else
            {
                return NotFound(new { ErrorMessage = $"Sorry! Book with {id} is not exist" });
            }
        }

        [HttpPost]
        public IActionResult Add(Book book)
        {
            if(book is not null)
            {
                Db.Books.Add(book);
                Db.SaveChanges();
                return Ok(new { Books = Db.Books.ToList(), Message = "Book Added Successfully!" });
            }
            else
            {
                return BadRequest(new { ErrorMessage = $"{book} not Valid!" });
            }
        }


        [HttpPut]
        public IActionResult Update(int id, [FromBody]Book book)
        {
            if(ModelState.IsValid == true)
            {
                var oldBook = Db.Books.FirstOrDefault(b => b.BookId == id);

                if(oldBook is not null)
                {
                    oldBook.Name = book.Name;
                    oldBook.Auther = book.Auther;
                    oldBook.Descrition = book.Descrition;
                    oldBook.Price = book.Price;
                    oldBook.Stock = book.Stock;
                    oldBook.CategoryId = book.CategoryId;
                    Db.SaveChanges();
                    return Ok(new { message = $"Book with {id} updated Successfully!" });
                }
                else
                {
                    return BadRequest(new { ErrorMessage = $"{book} not Valid!" });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oldBook = Db.Books.FirstOrDefault(b => b.BookId == id);

            if(oldBook is not null )
            {
                try
                {
                    Db.Books.Remove(oldBook);
                    Db.SaveChanges();
                    return Ok(new { oldBook, ErrorMessage = $"Book with id {id} Removed Successfully" });
                }
                catch (Exception ex)
                {
                    BadRequest(new { ErrorMessage = $"Book with {id} didn't remove! Please try again!" });
                }
            }
            return BadRequest(new { ErrorMessage = $"Book with {id} didn't remove or Id is not found! Please try again!" });
        }
    }
}

