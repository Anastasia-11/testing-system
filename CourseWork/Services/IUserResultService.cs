using CourseWork.Models;

namespace CourseWork.Services;

public interface IUserResultService
{
    IQueryable<UserResult> UserResults { get; }
    Task<UserResult?> GetByIdAsync(Guid? id);
    Task AddAsync(UserResult userResult);
    Task UpdateAsync(UserResult userResult);
    Task DeleteAsync(Guid? id);
}