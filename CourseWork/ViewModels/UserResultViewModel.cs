using CourseWork.Models;

namespace CourseWork.ViewModels;

public class UserResultViewModel
{
    public UserResult UserResult { get; set; } = new();
    public List<Question> Questions { get; set; } = new ();
}