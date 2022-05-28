using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class TestService : ITestService
{
    private readonly ApplicationContext _applicationContext;

    public TestService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public IQueryable<Test> Tests => _applicationContext.Tests;

    public async Task<Test?> GetByIdAsync(Guid? id)
    {
        return await Tests.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Test test)
    {
        await _applicationContext.AddAsync(test);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Test test)
    {
        _applicationContext.Update(test);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var test = await GetByIdAsync(id);
        if (test == null) return;
        _applicationContext.Remove(test);
        await _applicationContext.SaveChangesAsync();
    }
}