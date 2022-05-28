using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Models;

public class Test
{
    [Key]
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime OpenTime { get; set; }
    [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    public DateTime CloseTime { get; set; }
    public int QuestionsCount { get; set; }
    public int QuestionsLimit { get; set; }
    public bool ShuffleQuestions { get; set; }
    
    public IEnumerable<Question>? Questions { get; set; }
    public IEnumerable<UserResult>? UserResults { get; set; }
}