using Ficha.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;



namespace Ficha.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext context;

        public BookService (LibraryContext _context)
        {
            this.context = _context;
        }

        public Book Create(Book newBook)
        {
            //Quando a Criação do Livro é feita, é necessário procurar se existe um Publisher com o mesmo ID
            //Para associar o Book com Publisher
            Publisher pub = context.Publishers.Find(newBook.Publisher.ID);
            if (pub is null)
            {
                throw new NotImplementedException("Publisher does not exist!");
            }
            else
            {
                newBook.Publisher = pub;
                context.Books.Add(newBook);
                context.SaveChanges();
                return newBook;
            }
        }

        public void DeleteByISBN(string isbn)
        {
            var book = context.Books.Find(isbn);
            if(book is null)
            {
                throw new NotImplementedException("Couldn't find a book with this isbn!");
            }
            else
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }

        public void Download(List<Book> bookList)
        {

            //var listBook = context.Books.ToList();
            //Save the current Books and publishers to a JSON File
            string jsonAllBooks = JsonSerializer.Serialize(bookList);
            System.IO.File.WriteAllText("./JSON/allBooks.json", jsonAllBooks);
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/allBooks.json");
                Results.File(bytes, "application/json", "AllBooks.json");
            }
            catch (FileNotFoundException e)
            {
                Results.NotFound(e.Message);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            var books = context.Books
                .Include(p => p.Publisher);
            return books;
        }

        public Book GetByAuthor(string author)
        {
            var book = context.Books
                .Include(p => p.Publisher)
                .FirstOrDefault(b => b.Author == author); //returns the first if there are many books with the same Author [it could exist!]
            return book;
        }

        public Book GetByISBN(string isbn)
        {
           var book = context.Books
                .Include(p => p.Publisher)
                .SingleOrDefault(b => b.ISBN == isbn); //returns Exception if there are many books with the same ISBN [it should not exist!]
            return book;
        }

        public void Update(string isbn, Book book)
        {
            var bookToUpdate = context.Books.SingleOrDefault(b => b.ISBN == isbn);
            if (bookToUpdate is null)
            {
                throw new NotImplementedException("Couldn't find a book with this isbn!");
            }
            else
            {
                bookToUpdate.ISBN = book.ISBN;
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Pages = book.Pages;
                bookToUpdate.Language = book.Language;

                context.SaveChanges();
            }
        }

        public void UpdatePublisher(string isbn, int pubId)
        {
            var bookToUpdatePublisher = context.Books.Find(isbn); //Encontra o livro para bookToUpdatePublisher
            var pubToUpdate = context.Publishers.Find(pubId); //Encontra o Publisher para igualar ao bookToUpdatePublisher

            if (pubToUpdate is null || bookToUpdatePublisher is null)
            {
                throw new NotImplementedException("Book nor Publisher dont exist!");
            }
            else
            {
                bookToUpdatePublisher.Publisher = pubToUpdate;
                context.SaveChanges();
            }
        }
    }
}
