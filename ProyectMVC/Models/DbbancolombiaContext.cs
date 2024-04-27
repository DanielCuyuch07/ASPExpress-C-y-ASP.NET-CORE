using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectMVC.Models;

public partial class DbbancolombiaContext : DbContext
{
    public DbbancolombiaContext()
    {
    }

    public DbbancolombiaContext(DbContextOptions<DbbancolombiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Inversione> Inversiones { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__885457EEABAD0DEB");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.IdInversion).HasColumnName("idInversion");
            entity.Property(e => e.NombreCliente).HasMaxLength(50);
            entity.Property(e => e.NumeroDeCuenta)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Saldo).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdInversionNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdInversion)
                .HasConstraintName("FK__Clientes__idInve__60A75C0F");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__C225F98D20B60F36");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Departamento1)
                .HasMaxLength(50)
                .HasColumnName("departamento");
        });

        modelBuilder.Entity<Inversione>(entity =>
        {
            entity.HasKey(e => e.IdInversion).HasName("PK__Inversio__50D915009773A5F7");

            entity.Property(e => e.IdInversion).HasColumnName("idInversion");
            entity.Property(e => e.NombreInversion)
                .HasMaxLength(50)
                .HasColumnName("nombreInversion");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__2EC8D2ACF746F68E");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D10534F8ED91A9").IsUnique();

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AddressPerson).HasMaxLength(100);
            entity.Property(e => e.Dpi)
                .HasColumnType("numeric(20, 0)")
                .HasColumnName("DPI");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__5B65BF9771459FD8");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
