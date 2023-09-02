using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<DataLatih> ListDataLatih { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
                
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DataLatih>()
            .Property(c => c.Kelas)
            .HasConversion<string>();
        
        modelBuilder.Entity<DataLatih>()
            .Property(c => c.Sudut)
            .HasConversion<int>();
        

        base.OnModelCreating(modelBuilder);
    }



}