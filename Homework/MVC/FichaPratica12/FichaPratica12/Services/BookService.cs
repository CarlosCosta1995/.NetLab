using FichaPratica12.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FichaPratica12.Services
{
    public class BookService : IBookService
    {
        //============ 3.c.i ============//

        private readonly LibraryContext context;
        public BookService (LibraryContext _context) 
        {
            this.context = _context;
        }

        //============ 3.c.ii ============//
        //Nota Pessoal: Apenas sao feitas as Queries SQL de pesquisa e seus devidos returnos
        public Book Create(Book book) 
        {
            //Livro foi inserido, mas temos de verificar se o Seu publisher existe na DB
            Publisher publisher = context.Publishers.Find(book.Publisher.Id);

            //caso o publisher seja null, ou seja, nao exista returnamos uma execção
            if (publisher is null)
            {
                throw new NotImplementedException("Este publisher nao existe!");
            }
            //Caso exista temos de associar o publisher ao livro
            //Addicionar o Livro na DB
            //Guardar/Applicar as mudanças na DB
            //returnar o livro addicionado
            else
            {
                book.Publisher = publisher;
                context.Books.Add(book);
                context.SaveChanges();
                return book;
            }
        }

        public void DeleteByISBN(string isbn)
        {
            var bookToDelete = context.Books.Find(isbn);
            if (bookToDelete is not null)
            { 
                context.Books.Remove(bookToDelete);
                context.SaveChanges();  
            }
        }

        public string Download()
        {
            var bookList = context.Books.ToList();
            string JsonFileWithAllBooks = JsonSerializer.Serialize(bookList);
            return JsonFileWithAllBooks;
            System.IO.File.WriteAllText("./JSON/AllBookList.json", JsonFileWithAllBooks);
            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("./JSON/AllBookList.json");
                Results.File(bytes, "application/json", "JsonFileWithAllBooks.json");
            }
            catch (FileNotFoundException e)
            {
                Results.NotFound(e.Message);
            }
        }

        public IEnumerable<Book> GetAll() //Nos slides 18  - EntityFramework
        {
            var books = context.Books
                .Include(p => p.Publisher);
            return books;
        }

        public IEnumerable<Book> GetByAuthor(string author)
        {
            //var booksWithAuthor = context.Books
            //     .Include(p => p.Publisher)
            //     .GroupBy(a => a.Author == author).ToList();
            // return (IEnumerable<Book>)bookList;

            //var booksWithAuthor = context.Books.Include(a => a.Author == author);
            var booksWithAuthor = context.Books
                .AsEnumerable()
                .Where(p => p.Author == author);
            if (booksWithAuthor is null)
            {
                throw new NotImplementedException("Livros com este Autor não existem!");
            }
            else
            {
                return (IEnumerable<Book>)booksWithAuthor;
            }
        }

        public Book GetByISBN(string isbn)//Nos slides 18  - EntityFramework
        {
            var book = context.Books
                .Include(p => p.Publisher)
                .SingleOrDefault(b => b.ISBN == isbn);
            return book;
        }

        public void Update(string isbn, Book book)
        {
            var bookToUpdate = context.Books
                .SingleOrDefault(b => b.ISBN == isbn);

            if (bookToUpdate == null)
            {
                throw new NotImplementedException("Livro não existe!");
            }
            else
            {
                Publisher pub = context.Publishers.Find(book.Publisher.Id);
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Pages = book.Pages;
                bookToUpdate.language = book.language;
                bookToUpdate.Publisher = pub; //nao é necessáro porque ja esta esta incluido quando é feita a querie
                
                context.SaveChanges();
            }
        }

        public void UpdatePublisher(string isbn, int publisherId)
        {
            var bookToUpdatePublisher = context.Books.Find(isbn);
            var publisherToUpdate = context.Publishers.Find(publisherId);

            if (bookToUpdatePublisher is null || publisherToUpdate is null)
            {
                throw new NotImplementedException("Nao existe um livro ou publisher!");
            }

            bookToUpdatePublisher.Publisher = publisherToUpdate;

            context.SaveChanges();
        }
    }
}
