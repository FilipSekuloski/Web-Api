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
    public class AuthorController : ControllerBase
    {
        private BooksDbContext dbContext;

        public AuthorController(BooksDbContext dbContext) 
        {
            this.dbContext=dbContext;//container injector
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var result= dbContext.Authors.Select(x=> new AuthorDto
            {
                Id= x.Id,
                Name= x.Name,
                DateOfBirth = x.BirthYear
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author=dbContext.Authors.AsNoTracking().FirstOrDefault(x=>x.Id==id);
            if(author==null)
            {
                return NotFound();  
            }
            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDto dto)
        {
            var author = new Author
            {
                BirthYear = dto.DateOfBirth,
                Name = dto.Name
            };
            dbContext.Authors.Add(author);
            dbContext.SaveChanges();
            return Ok(author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            var toUpdate= dbContext.Authors.FirstOrDefault(x=>x.Id==id);
            if(toUpdate==null)
            {
                return NotFound();
            }
            toUpdate.Name= author.Name;
            toUpdate.BirthYear= author.BirthYear;
            dbContext.SaveChanges();
            return Ok(author);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author=dbContext.Authors.FirstOrDefault(x=>x.Id== id);
            if(author==null)
            {
                return NotFound();
            }
            dbContext.Authors.Remove(author);
            dbContext.SaveChanges();
            return Ok(author);
        }


    }
}
