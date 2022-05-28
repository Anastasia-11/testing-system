using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class QuestionService : IQuestionService
{
    private readonly ApplicationContext _applicationContext;

    public QuestionService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public IQueryable<Question> Questions => _applicationContext.Questions;

    public async Task<Question?> GetByIdAsync(Guid? id)
    {
        return await Questions.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task AddAsync(Question question)
    {
        await _applicationContext.AddAsync(question);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Question question)
    {
        _applicationContext.Update(question);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var question = await GetByIdAsync(id);
        if (question == null) return;
        _applicationContext.Remove(question);
        await _applicationContext.SaveChangesAsync();
    }
}