using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;
using Tempus.Core.Entities.User;

namespace Tempus.Data.Context;

public class TempusDbContext : DbContext
{
    public TempusDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<UserPhoto> UserPhotos => Set<UserPhoto>();
    public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
    public DbSet<GroupCategory> GroupCategories => Set<GroupCategory>();
    public DbSet<UserCategory> UserCategories => Set<UserCategory>();
    public DbSet<GroupPhoto> GroupPhotos => Set<GroupPhoto>();
    public DbSet<Group> Groups => Set<Group>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}