namespace Ficha14.Models
{
    public class User //Correspondency to the user table in the DB
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

    }
}
