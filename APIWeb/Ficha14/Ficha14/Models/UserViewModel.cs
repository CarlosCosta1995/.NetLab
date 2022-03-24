namespace Ficha14.Models
{
    public class UserViewModel //DTO == Data Transfer Object
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; } //Naoqueremos enviar a password apra a View(front-end)
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
