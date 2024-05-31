using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoASPTest.API.Models;

public class PlayerRegisterFormDTO
{
    [DisplayName("Prénom")]
    [DataType(DataType.Text)]
    [Required]
    public string FirstName { get; set; } = null!;

    [DisplayName("Nom")]
    [DataType(DataType.Text)]
    [Required]
    public string LastName { get; set; } = null!;

    [DisplayName("Email")]
    [DataType(DataType.EmailAddress)]
    [Required]
    public string Email { get; set; } = null!;

    [DisplayName("Mot de passe")]
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; } = null!;
}
