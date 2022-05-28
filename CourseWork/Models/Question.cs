using System.ComponentModel.DataAnnotations;
using CourseWork.Models.Enums;

namespace CourseWork.Models;

public class Question
{
    [Key]
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public int Cost { get; set; }
    
    public Guid TestId { get; set; }
    public Test? Test { get; set; }
    
    public IEnumerable<Answer>? Answers { get; set; }
}