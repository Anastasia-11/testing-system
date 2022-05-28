using CourseWork.Models;

namespace CourseWork.Services;

public interface ISuggestionService
{
    IQueryable<Suggestion> Suggestions { get; }
    Task<Suggestion?> GetByIdAsync(Guid? id);
    Task AddAsync(Suggestion suggestion);
    Task UpdateAsync(Suggestion suggestion);
    Task DeleteAsync(Guid? id);
}