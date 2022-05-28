using CourseWork.Models;
using CourseWork.Models.Enums;

namespace CourseWork.ViewModels;

public class CreateTestViewModel
{
    public Guid TestId { get; set; } = Guid.NewGuid();
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public int QuestionsLimit { get; set; }
    public bool ShuffleQuestions { get; set; }
    public List<Question> Questions { get; set; } = new();
    public List<QuestionType> Types { get; set; } = new();

    public CreateTestViewModel()
    {
        foreach (var type in Enum.GetValues(typeof(QuestionType)))
        {
            Types.Add((QuestionType)type);
        }
    }
}