using AiAnswerBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace AiAnswerBackend.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<App> Apps { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<ScoringResult> ScoringResults { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}