using Ficha12.Models;
using Microsoft.EntityFrameworkCore;

namespace Ficha12.Service
{
    public class BookService : IBookService 
        //implementa os servicos do IBookService para serem codificados 
        //Implementação do CRUD
    {
        //Using Dependicy Injection
        private readonly LibraryContext context;

        //Parameter Contructor
        public BookService(LibraryContext context) 
        { 
            this.context = context; // context == sessao á Base dados
        }

        //Implementing the IBookService Interface (Override Endpoints)
        public IEnumerable<Book> GetAllBooks() //GetAll
        {
            // Books == é todas as entradas na tabela
            //Books.Include == (inclued) obriga addicao a todos os objectos os dados da tabela publisher (uma tabela gigante com todos os dados)
            //[carregamente em lazyLoading, nao necessita de fazer joins apenas pesquisa]

            // return context.Books; Returna null no publisher pois nao foi addicionado/incluido na visualizaçao da tabela.

            var books = context.Books.Include(p => p.Publisher);
            return books;
        }
        public Book? GetByISBN(string isbn)
        {
            var book = context.Books
            .Include(b => b.Publisher)
            .SingleOrDefault(b => b.ISBN == isbn);

            return book;
        }
        
        public Book Create(Book newBook)
        {
            Publisher? publisher = context.Publishers.Find(newBook.Publisher.ID);
            if (publisher == null)
            {
                throw new NotImplementedException("Publisher does not exist.");
            }
            else
            {
                newBook.Publisher = publisher;
                context.Books.Add(newBook);
                context.SaveChanges();
                return newBook;
            }
        }
        public void GetByAuthor(string author)
        {
            throw new NotImplementedException();
        }
        public void Update(string isbn, Book book)
        {
            throw new NotImplementedException();
        }
        public void UpdatePublisher(string isbn, int publisherId)
        {
            throw new NotImplementedException();
        }
        public void Delete(string isbn)
        {
            throw new NotImplementedException();
        }

    }
}
