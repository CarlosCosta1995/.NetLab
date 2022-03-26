namespace Ficha122.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Pages { get; set; }

        public Publisher Publisher { get; set; }
    }
}
