using CourseWork.Database;
using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class SuggestionService : ISuggestionService
{
    private readonly ApplicationContext _applicationContext;

    public SuggestionService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public IQueryable<Suggestion> Suggestions => _applicationContext.Suggestions;

    public async Task<Suggestion?> GetByIdAsync(Guid? id)
    {
        return await Suggestions.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Suggestion suggestion)
    {
        await _applicationContext.AddAsync(suggestion);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Suggestion suggestion)
    {
        _applicationContext.Update(suggestion);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid? id)
    {
        var suggestion = await GetByIdAsync(id);
        if (suggestion == null) return;
        _applicationContext.Remove(suggestion);
        await _applicationContext.SaveChangesAsync();
    }
}