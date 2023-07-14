using System;
using System.Collections.Generic;
using investments_tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace investments_tracker;

public partial class InvestmentsTrackerContext : DbContext
{
    public DbSet<Broker> Brokers { get; set; }
    public DbSet<Deposit> Deposits { get; set; }


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
            
            entity
                .HasMany(e => e.Deposits)
                .WithOne(e => e.Broker)
                .HasForeignKey(e => e.BrokerId)
                .IsRequired();
        });

        modelBuilder.Entity<Deposit>(entity => { 
            entity.ToTable("deposits");
            
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v4()");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("now()");
            
            entity.Property(e => e.Amount)
                .IsRequired()
                .HasColumnName("amount");

            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date");
            
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
