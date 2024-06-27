using ABPD_TEST_2.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPD_TEST_2.Helpers;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<DriverCompetition> DriverCompetitions { get; set; }
    public DbSet<CarManufacturer> CarManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarManufacturer>()
            .Property(cm => cm.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<CarManufacturer>()
            .HasIndex(cm => cm.Name)
            .IsUnique();
        
        modelBuilder.Entity<CarManufacturer>()
            .HasMany(cm => cm.Cars)
            .WithOne(c => c.CarManufacturer)
            .HasForeignKey(c => c.CarManufacturerId);

        modelBuilder.Entity<Car>()
            .Property(c => c.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Drivers)
            .WithOne(d => d.Car)
            .HasForeignKey(d => d.CarId);

        modelBuilder.Entity<Driver>()
            .Property(d => d.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<Driver>()
            .HasMany(d => d.DriverCompetitions)
            .WithOne(dc => dc.Driver)
            .HasForeignKey(dc => dc.DriverId);

        modelBuilder.Entity<Competition>()
            .Property(c => c.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Competition>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        modelBuilder.Entity<Competition>()
            .HasMany(c => c.DriverCompetitions)
            .WithOne(dc => dc.Competition)
            .HasForeignKey(dc => dc.CompetitionId);

        modelBuilder.Entity<DriverCompetition>()
            .Property(dc => dc.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<DriverCompetition>()
            .HasKey(dc => new { dc.DriverId, dc.CompetitionId });

        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Driver)
            .WithMany(d => d.DriverCompetitions)
            .HasForeignKey(dc => dc.DriverId);

        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Competition)
            .WithMany(c => c.DriverCompetitions)
            .HasForeignKey(dc => dc.CompetitionId);
        
        // Seed data
        modelBuilder.Entity<CarManufacturer>().HasData(
                new CarManufacturer { Id = 1, Name = "Ferrari", RowVersion = new byte[0] },
                new CarManufacturer { Id = 2, Name = "Mercedes", RowVersion = new byte[0] },
                new CarManufacturer { Id = 3, Name = "Red Bull Racing", RowVersion = new byte[0] }
        );
        
        modelBuilder.Entity<Car>().HasData(
            new Car { Id = 1, CarManufacturerId = 1, ModelName = "SF21", Number = 5, RowVersion = new byte[0] },
            new Car { Id = 2, CarManufacturerId = 1, ModelName = "SF21", Number = 16, RowVersion = new byte[0] },
            new Car { Id = 3, CarManufacturerId = 2, ModelName = "W12", Number = 44, RowVersion = new byte[0] }
        );
    
        modelBuilder.Entity<Driver>().HasData(
            new Driver { Id = 1, FirstName = "Charles", LastName = "Leclerc", Birthday = new DateTime(1997, 10, 16), CarId = 2, RowVersion = new byte[0] },
            new Driver { Id = 2, FirstName = "Carlos", LastName = "Sainz", Birthday = new DateTime(1994, 9, 1), CarId = 1, RowVersion = new byte[0] },
            new Driver { Id = 3, FirstName = "Lewis", LastName = "Hamilton", Birthday = new DateTime(1985, 1, 7), CarId = 3, RowVersion = new byte[0] }
        );
    
        modelBuilder.Entity<Competition>().HasData(
            new Competition { Id = 1, Name = "Monaco Grand Prix", RowVersion = new byte[0] },
            new Competition { Id = 2, Name = "British Grand Prix", RowVersion = new byte[0] },
            new Competition { Id = 3, Name = "Italian Grand Prix", RowVersion = new byte[0] }
        );

        modelBuilder.Entity<DriverCompetition>().HasData(
            new DriverCompetition
                { DriverId = 1, CompetitionId = 1, Date = new DateTime(2023, 5, 23), RowVersion = new byte[0] },
            new DriverCompetition
                { DriverId = 2, CompetitionId = 1, Date = new DateTime(2023, 5, 23), RowVersion = new byte[0] },
            new DriverCompetition
                { DriverId = 3, CompetitionId = 1, Date = new DateTime(2023, 5, 23), RowVersion = new byte[0] });
        
        base.OnModelCreating(modelBuilder);
    }
}