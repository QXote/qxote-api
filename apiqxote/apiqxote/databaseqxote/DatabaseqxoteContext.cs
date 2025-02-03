using System;
using System.Collections.Generic;
using apiqxote.Models;
using Microsoft.EntityFrameworkCore;

namespace apiqxote.databaseqxote;

public partial class DatabaseqxoteContext : DbContext
{
    public DatabaseqxoteContext()
    {
    }

    public DatabaseqxoteContext(DbContextOptions<DatabaseqxoteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<BioConcentration> BioConcentrations { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<Tree> Trees { get; set; }

    public virtual DbSet<TreeName> TreeNames { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=QxoteDbOld;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.AnimalId);

            entity.ToTable("animal");

            entity.HasIndex(e => e.Zone, "fk_animal_zone1");

            entity.Property(e => e.AnimalId)
                .HasColumnName("animal_id");
            entity.Property(e => e.Abudance)
                .HasMaxLength(45)
                .HasColumnName("abudance"); 
            entity.Property(e => e.CloudCover)
                .HasColumnName("cloud_cover"); 
            entity.Property(e => e.Coordinates)
                .HasMaxLength(90)
                .HasColumnName("coordinates"); 
            entity.Property(e => e.Coverboards)
                .HasMaxLength(45)
                .HasColumnName("coverboards"); 
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date"); 
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time"); 
            entity.Property(e => e.SpeciesName)
                .HasMaxLength(45)
                .HasColumnName("species_name"); 
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time"); 
            entity.Property(e => e.Temperature)
                .HasColumnName("temperature"); 
            entity.Property(e => e.WindSpeed)
                .HasColumnName("wind_speed"); 
            entity.Property(e => e.Zone)
                .HasMaxLength(1)
                .HasColumnName("zone");

            entity.HasOne(d => d.ZoneNavigation)
                .WithMany(p => p.Animals)
                .HasForeignKey(d => d.Zone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_zone1");
        });

        modelBuilder.Entity<BioConcentration>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("bio_concentration");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Bcf)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("bcf");
            entity.Property(e => e.Cf)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("cf");
            entity.Property(e => e.Ctree)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("ctree");
            entity.Property(e => e.R)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("r");
            entity.Property(e => e.Species)
                .HasMaxLength(45)
                .HasColumnName("species");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId);

            entity.ToTable("plant");

            entity.HasIndex(e => e.Zone, "fk_plant_zone1");

            entity.Property(e => e.PlantId)
                .HasColumnName("plant_id");
            entity.Property(e => e.Coordinate)
                .HasMaxLength(90)
                .HasColumnName("coordinate"); 
            entity.Property(e => e.Cover)
                .HasMaxLength(45)
                .HasColumnName("cover"); 
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date"); 
            entity.Property(e => e.Humidity)
                .HasColumnName("humidity"); 
            entity.Property(e => e.PlotNr)
                .HasMaxLength(4)
                .HasColumnName("plot_nr"); 
            entity.Property(e => e.Species)
                .HasMaxLength(45)
                .HasColumnName("species"); 
            entity.Property(e => e.Temperature)
                .HasMaxLength(45)
                .HasColumnName("temperature"); 
            entity.Property(e => e.Zone)
                .HasMaxLength(1)
                .HasColumnName("zone");

            entity.HasOne(d => d.ZoneNavigation)
                .WithMany(p => p.Plants)
                .HasForeignKey(d => d.Zone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_plant_zone1");
        });

        modelBuilder.Entity<Tree>(entity =>
        {
            entity.HasKey(e => new { e.TreeNr, e.Zone });

            entity.ToTable("tree");

            entity.HasIndex(e => e.BioConcentrationId, "fk_tree_bio_concentration1");
            entity.HasIndex(e => e.TreeName, "fk_tree_tree_name1");
            entity.HasIndex(e => e.Zone, "fk_tree_zone1");

            entity.Property(e => e.TreeNr)
                .ValueGeneratedOnAdd()
                .HasColumnName("tree_nr");
            entity.Property(e => e.Zone)
                .HasMaxLength(1)
                .HasColumnName("zone");
            entity.Property(e => e.BioConcentrationId)
                .HasColumnName("bio_concentration_id");
            entity.Property(e => e.Circumference)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("circumference");
            entity.Property(e => e.Coordinate)
                .HasMaxLength(90)
                .HasColumnName("coordinate");
            entity.Property(e => e.Height)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("height");
            entity.Property(e => e.TreeName)
                .HasMaxLength(45)
                .HasColumnName("tree_name");
            entity.Property(e => e.Volume)
                .HasColumnType("decimal(10,2)")
                .HasColumnName("volume");

            entity.HasOne(d => d.BioConcentration)
                .WithMany(p => p.Trees)
                .HasForeignKey(d => d.BioConcentrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tree_bio_concentration1");

            entity.HasOne(d => d.TreeNameNavigation)
                .WithMany(p => p.Trees)
                .HasForeignKey(d => d.TreeName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tree_tree_name1");

            entity.HasOne(d => d.ZoneNavigation)
                .WithMany(p => p.Trees)
                .HasForeignKey(d => d.Zone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tree_zone1");
        });

        modelBuilder.Entity<TreeName>(entity =>
        {
            entity.HasKey(e => e.TreeName1);

            entity.ToTable("tree_name");

            entity.Property(e => e.TreeName1)
                .HasMaxLength(45)
                .HasColumnName("tree_name");
            entity.Property(e => e.CoordinateCount)
                .HasColumnName("coordinate_count");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.Zone1);

            entity.ToTable("zone");

            entity.Property(e => e.Zone1)
                .HasMaxLength(1)
                .HasColumnName("zone");
            entity.Property(e => e.Area)
                .HasColumnName("area");
            entity.Property(e => e.Classification)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("classification");
            entity.Property(e => e.Plots)
                .HasColumnName("plots");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
