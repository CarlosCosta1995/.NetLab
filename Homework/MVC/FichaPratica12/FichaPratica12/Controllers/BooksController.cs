using FichaPratica12.Models;
using FichaPratica12.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FichaPratica12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService service; // 4.b

        public BooksController(IBookService _service)  // 4.c
        {
            this.service = _service;
        }

        // ============ Todos os pedidos para a DB ========/

        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Book> Get() // 4.d.i
        {
            return service.GetAll();
        }

        // GET By ISBN
        [HttpGet("{isbn}", Name = "GetByISBN")]
        public IActionResult GetByISBN(string isbn) // 4.d.iv
        {
            //Instanciar um novo livro para procurar na DB
            Book book = service.GetByISBN(isbn);
            if (book is null)
            {
                return NotFound("Book not found!");
            }
            else
            {
                return Ok(book);
            }
        }

        // GET By Author
        [HttpGet("ByAuthor/{author}", Name = "GetByAuthor")]
        public IEnumerable<Book> GetByAuthor(string author) // 4.d.vi
        {
            return service.GetByAuthor(author);
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] Book newbook) // 4.d.i
        {
            if(newbook is not null)
            {
                Book book = service.Create(newbook);
                return CreatedAtRoute("GetByISBN", new { isbn = book.ISBN }, book); //Nao entendi?
            }
            else 
            { 
                return BadRequest();
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{isbn}", Name = "UpdateBook")]
        public IActionResult UpdateBook(string isbn, [FromBody] Book bookToUpdate) // 4.d.v
        {
            Book book = service.GetByISBN(isbn);
            if (bookToUpdate is not null || book is not null)
            {
                service.Update(isbn, bookToUpdate);
                return Ok(bookToUpdate);
            }
            else 
            {
                return BadRequest();
            }
        }

        // Patch
        [HttpPatch("{isbn}/publisherId", Name = "UpdateBookPublisher")]
        public IActionResult UpdateBookPublisher(string isbn,int pusblisherId) // 4.d.viii
        {
            Book book = service.GetByISBN(isbn);
            if (book is not null)
            {
                service.UpdatePublisher(isbn, pusblisherId);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{isbn}")]
        public IActionResult Delete(string isbn) // 4.d.iii
        {
            Book book = service.GetByISBN(isbn);
            if (book is not null)
            {
                service.DeleteByISBN(isbn);
                return Ok(book.ISBN);
            }
            else 
            { 
                return BadRequest(); 
            }
        }

        // Download
        [HttpGet("download", Name = "DownloadBookList")]
        public IActionResult DownloadBookList() // 4.d.vii
        {
            string JsonFileWithAllBooks = service.Download();
            System.IO.File.WriteAllText("./JSON/AllBookList.json", JsonFileWithAllBooks);
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/AllBookList.json");
                return File(bytes, "application/json", "JsonFileWithAllBooks.json");
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
