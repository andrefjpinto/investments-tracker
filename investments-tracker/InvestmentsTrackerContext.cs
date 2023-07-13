using System;
using System.Collections.Generic;
using investments_tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace investments_tracker;

public partial class InvestmentsTrackerContext : DbContext
{
    public DbSet<Broker> Brokers { get; set; } = null!;

    public InvestmentsTrackerContext()
    {
    }

    public InvestmentsTrackerContext(DbContextOptions<InvestmentsTrackerContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Broker>(entity =>
        {
            entity.ToTable("brokers");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name");

            entity.Property(e => e.Website)
                .IsRequired()
                .HasColumnName("website");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
