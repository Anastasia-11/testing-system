using CourseWork.Models;

namespace CourseWork.Services;

public interface IQuestionService
{
    IQueryable<Question> Questions { get; }
    Task<Question?> GetByIdAsync(Guid? id);
    Task AddAsync(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(Guid? id);
}