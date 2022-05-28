using CourseWork.Models.Enums;

namespace CourseWork.Extensions;

public static class QuestionTypeExtensions
{
    public static string ToFriendlyString(this QuestionType type)
    {
        return type switch
        {
            QuestionType.WithAnswerOptions => "С вариантами ответов",
            QuestionType.FreeEntry => "Свободный ввод",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}