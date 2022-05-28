using CourseWork.Models;

namespace CourseWork.Services;

public interface IUserAnswerService
{
    IQueryable<UserAnswer> UserAnswers { get; }
    Task<UserAnswer?> GetByIdAsync(Guid? id);
    Task AddAsync(UserAnswer userAnswer);
    Task UpdateAsync(UserAnswer userAnswer);
    Task DeleteAsync(Guid? id);
}