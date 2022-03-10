using Ficha12.Models;
using Microsoft.EntityFrameworkCore;

namespace Ficha12.Service
{
    public class BookService : IBookService
    {
        //Using Dependicy Injection
        private readonly LibraryContext context;

        //Parameter Contructor
        public BookService(LibraryContext context) 
        { 
            this.context = context; 
        }

        //Implementing the IBookService Interface (Override Endpoints)
        public IEnumerable<Book> GetAllBooks() //GetAll
        {
            var books = context.Books.Include(p => p.Publisher);
            return books;
        }
        public Book GetByISBN(int isbn)
        {
            var book = context.Books.Include(b => b.Publisher).SingleOrDefault(b => b.ISBN == isbn);
            return book;
        }
        
        public Book Create(Book newBook)
        {
            throw new NotImplementedException();
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
