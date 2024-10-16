using System.ComponentModel.DataAnnotations;

namespace TenexCars.DTOs
{
    public class LoginDto
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "UserName is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
