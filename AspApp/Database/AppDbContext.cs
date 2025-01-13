using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspApp.Database;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id).ValueGeneratedOnAdd()
            ;
        //modelBuilder.Entity<User>().HasData(Seeder.GenerateUsers(100));
            
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }
}


public class Seeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(GenerateUsers(100));
            dbContext.SaveChanges();
        }
    }
    
    public static IEnumerable<User> GenerateUsers(int count)
    {
        var hasher = new PasswordHasher<User>();
        var faker = new Faker<User>()
                // .RuleFor(u => u.Id, f => f.Random.Number(0, 1000))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, (f, user) => hasher.HashPassword(user, f.Internet.Password()))
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
            ;
        return faker.Generate(count);
    }
}