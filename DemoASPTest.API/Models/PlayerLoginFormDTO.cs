using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoASPTest.API.Models;

public class PlayerLoginFormDTO
{
    [DisplayName("Email")]
    [Required]
    public string Email { get; set; } = null!;

    [DisplayName("Mot de passe")]
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;
}
