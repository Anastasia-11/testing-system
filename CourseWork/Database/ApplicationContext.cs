using CourseWork.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Database;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    
    public ApplicationContext() {}

    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();
    public DbSet<UserResult> UserResults => Set<UserResult>();
    public DbSet<Suggestion> Suggestions => Set<Suggestion>();
}