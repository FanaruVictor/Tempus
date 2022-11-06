using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;

namespace Tempus.Data.Context;

public class TempusDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public TempusDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}