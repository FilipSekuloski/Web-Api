using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vezbi.Data;
using Vezbi.Dtos;
using Vezbi.Entities;

namespace Vezbi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private BooksDbContext dbContext;

        public BookController(BooksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var result = dbContext.Books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = dbContext.Authors.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
            };
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var toUpdate = dbContext.Books.FirstOrDefault(x => x.Id == id);
            if (toUpdate == null)
            {
                return NotFound();
            }
            toUpdate.Title = book.Title;
            toUpdate.Description = book.Description;
            toUpdate.Price = book.Price;
            dbContext.SaveChanges();
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = dbContext.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
            return Ok(book);
        }
    }
}