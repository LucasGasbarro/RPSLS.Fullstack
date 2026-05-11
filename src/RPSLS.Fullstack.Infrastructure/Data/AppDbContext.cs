using Microsoft.EntityFrameworkCore;
using RPSLS.Fullstack.Api.Data.Entities;

namespace RPSLS.Fullstack.Api.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ScoreEntryEntity> ScoreEntries => Set<ScoreEntryEntity>();
    public DbSet<PlayerRankEntity> PlayerRanks => Set<PlayerRankEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ScoreEntryEntity>(entity =>
        {
            entity.ToTable("ScoreEntries");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Result).IsRequired();
            entity.Property(x => x.CreatedAtUtc).IsRequired();
            entity.HasIndex(x => x.CreatedAtUtc);
        });

        modelBuilder.Entity<PlayerRankEntity>(entity =>
        {
            entity.ToTable("PlayerRanks");
            entity.HasKey(x => x.NormalizedName);
            entity.Property(x => x.NormalizedName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.DisplayName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Points).IsRequired();
            entity.Property(x => x.UpdatedAtUtc).IsRequired();
            entity.HasIndex(x => x.Points);
        });

        base.OnModelCreating(modelBuilder);
    }
}
