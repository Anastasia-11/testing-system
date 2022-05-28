using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class UserAnswerService : IUserAnswerService
{
    private readonly ApplicationContext _applicationContext;

    public UserAnswerService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public IQueryable<UserAnswer> UserAnswers => _applicationContext.UserAnswers;

    public async Task<UserAnswer?> GetByIdAsync(Guid? id)
    {
        return await UserAnswers.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(UserAnswer userAnswer)
    {
        await _applicationContext.AddAsync(userAnswer);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserAnswer userAnswer)
    {
        _applicationContext.Update(userAnswer);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var userAnswer = await GetByIdAsync(id);
        if (userAnswer == null) return;
        _applicationContext.Remove(userAnswer);
        await _applicationContext.SaveChangesAsync();
    }
}