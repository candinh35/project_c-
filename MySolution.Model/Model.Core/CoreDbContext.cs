using Microsoft.EntityFrameworkCore;
using Model.Core.Entities;

namespace Model.Core;

public partial class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions options) : base(options) {}

    public virtual DbSet<User>? Users { get; set; }
}