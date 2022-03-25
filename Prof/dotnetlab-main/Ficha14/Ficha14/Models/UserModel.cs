using System.ComponentModel.DataAnnotations;

namespace Ficha14.Models
{
    public class UserModel
    {

        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        public string Path { get; set; }
    }
    //public class ImageUploaded
    //{
    //    public string Path { get; set; }
    //}
}
