using JediArchives.DataStorage.EfModels;
using Microsoft.EntityFrameworkCore;

namespace JediArchives.DataStorage;

public class DataContextWrite : DbContext {
    public DataContextWrite(DbContextOptions<DataContextWrite> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
}
