using Ficha12.Models;

namespace Ficha12.Data
{
    public static class LibraryDBInitializer
    {
        public static void InsertData(LibraryContext context)
        {
            //Adds a publisher
            var publisher = new Publisher
            {
                Name = "Mariner Books"
            }; 
            context.Publishers.Add(publisher);

            //Adds some books
            context.Books.Add(new Book
            {
                ISBN = "978-054400415",
                Title = "The Lord of the Rings",
                Author = "J.R.R Tolkien",
                Language = "English",
                Pages = "1216",
                Publisher = publisher
            });
            context.SaveChanges(); 
        }
    }
}
