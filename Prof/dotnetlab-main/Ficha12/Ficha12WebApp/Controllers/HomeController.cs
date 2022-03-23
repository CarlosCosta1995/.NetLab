using Ficha12.Models;
using Ficha12WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ficha12WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService service;

        public HomeController(IBookService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            var books = service.GetAll();
            return View(new BooksViewModel { Books = books });
        }

        //public IActionResult Index()
        //{
        //    //Iniciando sem o codigo a cima do index, a api nao funciona.
        //    //A ligaçao da com a DB através da api nao esta a ser feita.
        //    IEnumerable<Book> books = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7240/api/"); //servidor neste link nao esta em execução
        //        //HTTP GET
        //        var responseTask = client.GetAsync("books");
        //        responseTask.Wait();

        //        var result = responseTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var task = result.Content.ReadFromJsonAsync<IEnumerable<Book>>();
        //            task.Wait();
        //            books = task.Result;
        //        }
        //        else //web api sent error response 
        //        {        
        //            books = Enumerable.Empty<Book>();
        //            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
        //        }
        //    }
        //    return View(new BooksViewModel { Books = books });
        //}


        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create() //Retorna a view apenas
        {
            return View();
        }

        [HttpPost] //Endpoint para o botao do formulario e mandar o book para a DB       
        public async Task<IActionResult> Create(Book book) //retorna o endpoint oriondo dos services [Post]
        {
            if (ModelState.IsValid) //ModelState Se o formulario é valido, se algum campo vem vazio => false
            {               
                var newBook = service.Create(book);              
                if(newBook is not null)
                    return RedirectToAction(nameof(Index)); //redirect para a view index
                else
                    return RedirectToAction(nameof(Error)); //redirect para a view de erro
            }
            else {
                return RedirectToAction(nameof(Error));
            }            
        }

        /// <summary>
        /// Criar uma Iction para ir a view do Edit
        /// Criar uma Iaction task para fazer Post do item na base dados
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit() //Retorna a view apenas
        {
            return View();
        }

        /*crtl+k ctrl+c // ctrl+k crtl+u*/
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var newBook = service.Update(book.ISBN, book);
                if (newBook is not null)
                    return RedirectToAction(nameof(Index)); //redirect para a view index
                else
                    return RedirectToAction(nameof(Error)); //redirect para a view de erro
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpDelete]
        public IActionResult Delete(string isbn)
        {
            if (ModelState.IsValid)
            {
                service.DeleteByISBN(isbn);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
    }
}