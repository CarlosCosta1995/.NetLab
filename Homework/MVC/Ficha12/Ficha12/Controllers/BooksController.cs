using Ficha12.Models;
using Ficha12.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService service;

        public BooksController(IBookService _service)
        {
            this.service = _service;
        }

        // Get All
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return service.GetAll();
        }

        // Get By ISBN
        [HttpGet("{isbn}", Name= "GetByISBN")]
        public IActionResult Get(string isbn)
        {
            Book book = service.GetByISBN(isbn);
            if (book is null)
            {
                return (IActionResult)Results.NotFound("Book not found!");
            }
            else
            {
                return (IActionResult)Results.Ok(book);
            }
        }

        // Get By Author
        [HttpGet("{isbn}", Name = "GetByAuthor")]
        public IActionResult GetByAuthor(string author)
        {
            Book book = service.GetByAuthor(author);
            if (book is null)
            {
                return (IActionResult)Results.NotFound("Book not found!");
            }
            else
            {
                return (IActionResult)Results.Ok(book);
            }
        }

        // Create (Post)
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book is not null)
            {
                Book newBook = service.Create(book);
                return (IActionResult)Results.CreatedAtRoute("GetByISBN", new { isbn = newBook.ISBN, newBook });
            }
            else
            {
                return (IActionResult)Results.BadRequest("The book could not be created, something is missing!");
            }
        }

        // Update Book (Put)
        [HttpPut("{isbn}", Name = "Update")]
        public IActionResult Update(string isbn, [FromBody] Book book)
        {
            var bookToUpdate = service.GetByISBN(isbn);
            if (book is not null || bookToUpdate is not null)
            {
                service.Update(isbn, book);
                return (IActionResult)Results.Ok();
            }
            else 
            { 
                return (IActionResult)Results.NotFound(); 
            }
        }

        // Update Publisher (Put)
        [HttpPatch("{isbn}/publisherId")]
        public IActionResult UpdatePublisher(string isbn, int publisherId)
        {
            var bookToUpdate = service.GetByISBN(isbn);
            if (bookToUpdate is not null)
            {
                service.UpdatePublisher(isbn, publisherId);
                return (IActionResult)Results.Ok();
            }
            else
            {
                return (IActionResult)Results.NotFound();
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{isbn}")]
        public IActionResult Delete(string isbn)
        {
            var bookToDelete = service.GetByISBN(isbn);
            if (bookToDelete is not null)
            {
                service.DeleteByISBN(isbn);
                return (IActionResult)Results.Ok();    
            }
            else
            {
              return (IActionResult)Results.NotFound();
            }
        }

        // Download Books List
        [HttpGet("download", Name = "GetDownload")]
        public IActionResult GetDownload()
        {
            var bookList = service.GetAll().ToList();
            if (bookList is not null)
            {
                service.Download(bookList);
                return (IActionResult)Results.Ok();
            }
            else
            {
                return (IActionResult)Results.BadRequest();
            }
            
        }
    }
}
