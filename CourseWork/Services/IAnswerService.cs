using CourseWork.Models;

namespace CourseWork.Services;

public interface IAnswerService
{
    IQueryable<Answer> Answers { get; }
    Task<Answer?> GetByIdAsync(Guid? id);
    Task AddAsync(Answer answer);
    Task UpdateAsync(Answer answer);
    Task DeleteAsync(Guid? id);
}