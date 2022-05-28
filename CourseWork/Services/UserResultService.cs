using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class UserResultService : IUserResultService
{
    private readonly ApplicationContext _applicationContext;

    public UserResultService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public IQueryable<UserResult> UserResults => _applicationContext.UserResults;

    public async Task<UserResult?> GetByIdAsync(Guid? id)
    {
        return await UserResults.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(UserResult userResult)
    {
        await _applicationContext.AddAsync(userResult);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserResult userResult)
    {
        _applicationContext.Update(userResult);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var userResult = await GetByIdAsync(id);
        if (userResult == null) return;
        _applicationContext.Remove(userResult);
        await _applicationContext.SaveChangesAsync();
    }
}