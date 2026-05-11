using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPSLS.Fullstack.Api.Data;

#nullable disable

namespace RPSLS.Fullstack.Api.Migrations;

[DbContext(typeof(AppDbContext))]
public partial class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.11");

        modelBuilder.Entity("RPSLS.Fullstack.Api.Data.Entities.ScoreEntryEntity", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            b.Property<DateTime>("CreatedAtUtc")
                .HasColumnType("TEXT");

            b.Property<int>("ComputerChoiceId")
                .HasColumnType("INTEGER");

            b.Property<string>("Result")
                .IsRequired()
                .HasColumnType("TEXT");

            b.Property<int>("PlayerChoiceId")
                .HasColumnType("INTEGER");

            b.HasKey("Id");

            b.HasIndex("CreatedAtUtc");

            b.ToTable("ScoreEntries");
        });

        modelBuilder.Entity("RPSLS.Fullstack.Api.Data.Entities.PlayerRankEntity", b =>
        {
            b.Property<string>("NormalizedName")
                .HasMaxLength(100)
                .HasColumnType("TEXT");

            b.Property<string>("DisplayName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("TEXT");

            b.Property<int>("Points")
                .HasColumnType("INTEGER");

            b.Property<DateTime>("UpdatedAtUtc")
                .HasColumnType("TEXT");

            b.HasKey("NormalizedName");

            b.HasIndex("Points");

            b.ToTable("PlayerRanks");
        });
#pragma warning restore 612, 618
    }
}
