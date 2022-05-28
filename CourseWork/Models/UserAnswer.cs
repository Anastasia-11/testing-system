using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models;

public class UserAnswer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public int QuestionNumber { get; set; }
    public int AnswerNumber { get; set; }
    [Comment("User text answer to a free entry type question")]
    public string AnswerText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public DateTime AnswerTime { get; set; }
    public bool IsAnswered { get; set; }
    
    public Guid UserResultId { get; set; }
    public UserResult? UserResult { get; set; }
}