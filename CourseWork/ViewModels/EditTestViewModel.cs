using CourseWork.Models;
using CourseWork.Models.Enums;

namespace CourseWork.ViewModels;

public class EditTestViewModel
{
    public Guid TestId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime CloseTime { get; set; }
    public int QuestionsLimit { get; set; }
    public int QuestionsCount { get; set; }
    public bool ShuffleQuestions { get; set; }
    public List<Question> Questions { get; set; }
    public List<QuestionType> Types { get; set; } = new();

    public EditTestViewModel()
    {
        foreach (var type in Enum.GetValues(typeof(QuestionType)))
        {
            Types.Add((QuestionType)type);
        }
    }
}