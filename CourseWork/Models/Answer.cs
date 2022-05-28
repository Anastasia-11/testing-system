using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models;

public class Answer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsCorrect { get; set; }
    public int Number { get; set; }
    public string? Response { get; set; }
    public string? CorrectResponse { get; set; }
    
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
}