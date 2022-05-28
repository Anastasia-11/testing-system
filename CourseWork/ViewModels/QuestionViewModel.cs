using CourseWork.Models;

namespace CourseWork.ViewModels;

public class QuestionViewModel
{
    public Question Question { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
    public List<UserAnswer> UserAnswers { get; set; } = new();
    public Guid? SelectedUserAnswerId { get; set; }
    public PageViewModel? PageViewModel { get; set; }
    public DateTime TestCloseTime { get; set; }
}