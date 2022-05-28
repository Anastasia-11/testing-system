using System.ComponentModel.DataAnnotations;

namespace CourseWork.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Не указано имя")] 
    [DataType(DataType.Text)]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Имя должно быть от 3 до 40 символов")]
    [Display(Name = "Имя")] public string Name { get; set; }

    [Required(ErrorMessage = "Не указан email")] 
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный email")]
    [Display(Name = "Email")] public string Email { get; set; }

    [Required(ErrorMessage = "Не указан номер телефона")]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"\+([0-9]{3})\(([0-9]{2})\)[0-9]{3}-[0-9]{2}-[0-9]{2}", ErrorMessage = "Не верный номер телефона")]
    [Display(Name = "Номер телефона")]
    public string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Не указана группа")]
    [Display(Name = "Номер группы")]
    public int Group { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Не указан пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}