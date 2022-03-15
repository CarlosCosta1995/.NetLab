using Ficha12.Models;
using Ficha12.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Dependency Injection
        private readonly IBookService service;

        //Constructor with the parameters (IBookService)
        public BooksController(IBookService service)
        {
            this.service = service;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return service.GetAllBooks();
        } 

        // GET api/<BookController>/5
        [HttpGet("{isbn}", Name="GetByISBN")]
        public IActionResult GetByISBN(string isbn)
        {
            var book = service.GetByISBN(isbn);
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        // POST api/<BookController>
        [HttpPost("", Name = "Create")]
        public IActionResult Create(Book newBook)
        {
            if (newBook == null)
            {
                return NotFound();
            }
            else
            {
                //igualar as atributos do livro
                return Ok(newBook);
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
