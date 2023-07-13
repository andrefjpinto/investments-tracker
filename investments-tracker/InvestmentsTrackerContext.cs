using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace investments_tracker;

public partial class InvestmentsTrackerContext : DbContext
{
    public InvestmentsTrackerContext()
    {
    }

    public InvestmentsTrackerContext(DbContextOptions<InvestmentsTrackerContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:InvestmentsTracker");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
