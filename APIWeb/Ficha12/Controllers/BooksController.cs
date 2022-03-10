using Ficha12.Models;
using Ficha12.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ficha12.Controllers
{
    public class BooksController : Controller
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
    }
}
