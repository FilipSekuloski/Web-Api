using Homework___Class_03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework___Class_03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<BookModel> books = new();

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpGet]
        public IActionResult GetBookByTitleAndAuthor(string title, string author)
        {
            var book = books.FirstOrDefault(x=>x.Author==author);
        }

        [HttpPost]
        public IActionResult CreateBoook(BookModel book)
        {
            book.Id = books.Any() ? books.Max(x => x.Id) + 1 : 1;
            books.Add(book);
            return Created("api/book", book);
        }
    }
}




//[HttpPost] // api/v1/note
//public async Task<IActionResult> CreateNoteAsync([FromBody] string note)
//{
//    context.Notes.Add(new Note
//    {
//        Title = note,
//    });
//    await context.SaveChangesAsync();
//    return Created("api/v1/note", note);
//}
