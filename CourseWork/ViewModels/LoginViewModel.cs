using System.ComponentModel.DataAnnotations;

namespace CourseWork.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}