using JediArchives.DataStorage.EfModels;
using Microsoft.EntityFrameworkCore;

namespace JediArchives.DataStorage;

public class DataContextRead : DbContext {
    public DataContextRead(DbContextOptions<DataContextRead> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}
