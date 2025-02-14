using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Model.Core.Entities;

namespace Model.Core;

public partial class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions options) : base(options) {}

    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<RefreshToken>? RefreshTokens { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Convert name table to snake_case
            entity.SetTableName(ToSnakeCase(entity.GetTableName()));

            // Convert name column to snake_case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
        }
    }

    private static string? ToSnakeCase(string? input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}