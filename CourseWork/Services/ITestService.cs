using CourseWork.Models;

namespace CourseWork.Services;

public interface ITestService
{
    IQueryable<Test> Tests { get; }
    Task<Test?> GetByIdAsync(Guid? id);
    Task AddAsync(Test test);
    Task UpdateAsync(Test test);
    Task DeleteAsync(Guid? id);
}