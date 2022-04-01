using FichaPratica12.Models;

namespace FichaPratica12.Services
{
    public interface IBookService // 3.a
    {
        public abstract Book Create(Book book); // 3.b.ii
        public abstract IEnumerable<Book> GetAll(); // 3.b.i
        public abstract Book GetByISBN(string isbn); // 3.b.iv
        public abstract IEnumerable<Book> GetByAuthor(string author); // 3.b.vi
        public abstract void Update(string isbn, Book book); // 3.b.v
        public abstract void UpdatePublisher(string isbn, int publisherId); // 3.b.viii
        public abstract void DeleteByISBN(string isbn); // 3.b.iii
        public abstract string Download(); // 3.b.vii
    }
}
