using System;
using CRMProject.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRMProject.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cari> Caris { get; set; }
    public DbSet<Malzeme> Malzemes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Cari tablosu konfigürasyonu
        builder.Entity<Cari>(entity =>
        {
            entity.HasIndex(e => e.CariKodu).IsUnique();
            entity.Property(e => e.CariKodu).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Ünvan).IsRequired().HasMaxLength(200);
            entity.Ignore(e => e.EPosta);
        });

        // Malzeme tablosu konfigürasyonu
        builder.Entity<Malzeme>(entity =>
        {
            entity.HasIndex(e => e.MalzemeKodu).IsUnique();
            entity.Property(e => e.MalzemeKodu).IsRequired().HasMaxLength(30);
            entity.Property(e => e.MalzemeAdı).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SatışFiyatı).HasPrecision(18, 2);
            entity.Property(e => e.AlışFiyatı).HasPrecision(18, 2);
            entity.Property(e => e.StokMiktarı).HasPrecision(18, 2);
            entity.Property(e => e.KritikStok).HasPrecision(18, 2);
        });
    }
}