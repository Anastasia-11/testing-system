using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models;

public class Suggestion
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Не указана тема")]
    [Display(Name = "Тема")]
    public string Theme { get; set; }
    
    [Required(ErrorMessage = "Не указан описание")]
    [Display(Name = "Описание")]
    public string Text { get; set; }
    
    public byte[]? ImageData { get; set; }
    
    public DateTime CreationDate { get; set; } = DateTime.Now;
}