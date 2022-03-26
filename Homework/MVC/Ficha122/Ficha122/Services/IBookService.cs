using Ficha122.Models;

namespace Ficha122.Services
{
    public interface IBookService
    {
        public abstract Book Create(Book newBook);
        public abstract IEnumerable<Book> GetAll();
        public abstract Book GetByISBN(string isbn);
        public abstract Book GetByAuthor(string author);
        public abstract void Update(string isbn, Book book);
        public abstract void UpdatePublisher(string isbn, int pubId);
        public abstract void DeleteByISBN(string isbn);
        public abstract void Download(List<Book> bookList);
    }
}
