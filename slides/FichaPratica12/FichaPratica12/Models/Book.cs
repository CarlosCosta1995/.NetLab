namespace FichaPratica12.Models
{
    // 2.b
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string language { get; set; }
        public int Pages { get; set; }
        public Publisher Publisher { get; set; }
    }
}
