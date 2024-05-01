using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JoaxacaTrackTraceContext : DbContext
{
    public JoaxacaTrackTraceContext()
    {
    }

    public JoaxacaTrackTraceContext(DbContextOptions<JoaxacaTrackTraceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<Repartidor> Repartidors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=JOaxacaTrackTrace;  TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquete__DE278F8B43D26099");

            entity.ToTable("Paquete");

            entity.Property(e => e.CodigoQr).HasColumnName("CodigoQR");
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DireccionOrigen)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FechaEstimadaEntrega).HasColumnType("date");
            entity.Property(e => e.InstruccionEntrega)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.NumeroGuia)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Peso).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Repartidor>(entity =>
        {
            entity.HasKey(e => e.IdRepartidor).HasName("PK__Repartid__BF0B3B9A360488BD");

            entity.ToTable("Repartidor");

            entity.Property(e => e.ApelldioMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97EB2566FF");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534970D9F38").IsUnique();

            entity.Property(e => e.ApelldioMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
