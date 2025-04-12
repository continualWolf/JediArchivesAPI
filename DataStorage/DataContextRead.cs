using JediArchives.DataStorage.EfModels;
using Microsoft.EntityFrameworkCore;

namespace JediArchives.DataStorage;

public class DataContextRead : DbContext {
    public DataContextRead(DbContextOptions<DataContextRead> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<SystemEntity> Systems { get; set; }
    public DbSet<Planet> Planets { get; set; }
    public DbSet<Polygon> Polygons { get; set; }
    public DbSet<CoordinatePoint> CoordinatePoints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // 🌌 Region → Sector (1-to-many)
        modelBuilder.Entity<Region>()
            .HasMany(r => r.Sectors)
            .WithOne(s => s.Region)
            .HasForeignKey(s => s.RegionId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🗺️ Sector → SystemEntity (1-to-many)
        modelBuilder.Entity<Sector>()
            .HasMany(s => s.Systems)
            .WithOne(st => st.Sector)
            .HasForeignKey(st => st.SectorId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🌀 SystemEntity → Planet (1-to-many)
        modelBuilder.Entity<SystemEntity>()
            .HasMany(st => st.Planets)
            .WithOne(p => p.System)
            .HasForeignKey(p => p.SystemEntityId)
            .OnDelete(DeleteBehavior.Cascade);

        // 📍 Polygon → CoordinatePoint (1-to-many, ordered)
        modelBuilder.Entity<Polygon>()
            .HasMany(p => p.Points)
            .WithOne(pt => pt.Polygon)
            .HasForeignKey(pt => pt.PolygonId)
            .OnDelete(DeleteBehavior.Cascade);

        // 👁️ Ensure order is indexed for rendering order
        modelBuilder.Entity<CoordinatePoint>()
            .HasIndex(p => new { p.PolygonId, p.Order });

        // 📌 Sector → Polygon (optional 1-to-1)
        modelBuilder.Entity<Sector>()
            .HasOne(s => s.Polygon)
            .WithMany()
            .HasForeignKey(s => s.PolygonId)
            .OnDelete(DeleteBehavior.SetNull);

        // 🌐 Region → Polygon (optional 1-to-1)
        modelBuilder.Entity<Region>()
            .HasOne(r => r.Polygon)
            .WithMany()
            .HasForeignKey(r => r.PolygonId)
            .OnDelete(DeleteBehavior.SetNull);

        // ✅ Configure required fields if needed
        modelBuilder.Entity<Region>()
            .Property(r => r.Name)
            .IsRequired();

        modelBuilder.Entity<Sector>()
            .Property(s => s.Name)
            .IsRequired();

        modelBuilder.Entity<SystemEntity>()
            .Property(st => st.Name)
            .IsRequired();

        modelBuilder.Entity<Planet>()
            .Property(p => p.Name)
            .IsRequired();

    }
}
