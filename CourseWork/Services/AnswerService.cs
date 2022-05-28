using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class AnswerService : IAnswerService
{
    private readonly ApplicationContext _applicationContext;

    public AnswerService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public IQueryable<Answer> Answers => _applicationContext.Answers;

    public async Task<Answer?> GetByIdAsync(Guid? id)
    {
        return await Answers.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Answer answer)
    {
        await _applicationContext.AddAsync(answer);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Answer answer)
    {
        _applicationContext.Update(answer);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var answer = await GetByIdAsync(id);
        if (answer == null) return;
        _applicationContext.Remove(answer);
        await _applicationContext.SaveChangesAsync();
    }
}