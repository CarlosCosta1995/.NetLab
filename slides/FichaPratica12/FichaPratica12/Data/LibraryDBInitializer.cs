using FichaPratica12.Models;

namespace FichaPratica12.Data
{
    public static class LibraryDBInitializer // 5.b
    {
        public static void InsertData(LibraryContext context) // 5.c
        {
            //Add Publisher
            var publisher = new Publisher
            {
                Name = "Mariner Books"
            };
            context.Publishers.Add(publisher);

            //Add some Books
            context.Books.Add(new Book
            {
                ISBN = "978-0544003415",
                Title = "The Lord of th Rings",
                Author = "J.R.R Tolkien",
                language = "English",
                Pages = 1216,
                Publisher = publisher
            });

            context.SaveChanges();  
        }
    }
}
