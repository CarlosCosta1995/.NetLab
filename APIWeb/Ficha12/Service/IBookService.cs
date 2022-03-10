using Ficha12.Models;
using System.Collections.Generic;

namespace Ficha12.Service
{
    public interface IBookService
    {
        //Criating Endpoints not highly bounded.
        public abstract IEnumerable<Book> GetAllBooks(); //GetAll
        public abstract Book GetByISBN(string isbn); //Get by ISBN
        public abstract Book Create(Book newBook); //Create
        public abstract void GetByAuthor(string author); //Get by Authors
        public abstract void Update(string isbn, Book book); //Update Book
        public abstract void UpdatePublisher(string isbn, int publisherId); //Update Publisher of a book
        public abstract void Delete(string isbn); //Delete By ISBN
    }
}
