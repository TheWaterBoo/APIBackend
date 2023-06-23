using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Modelos;

public partial class BackendapiContext : DbContext
{
    public BackendapiContext()
    {
    }

    public BackendapiContext(DbContextOptions<BackendapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__cliente__C2FF24BD71714540");

            entity.ToTable("cliente");

            entity.Property(e => e.ClienteId).HasColumnName("clienteID");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Estado)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.PersonaId).HasColumnName("personaId");

            entity.HasOne(d => d.oPersona).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cliente__cliente__398D8EEE");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cuenta__3213E83F0C6ABBAF");

            entity.ToTable("cuenta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("clienteId");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.NumeroCuenta).HasColumnName("numeroCuenta");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldoInicial");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipoCuenta");

            entity.HasOne(d => d.oCliente).WithOne(p => p.Cuenta)
                .HasForeignKey<Cuenta>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuenta__id__3C69FB99");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movimien__3213E83F1AAF15B8");

            entity.ToTable("movimientos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CuentaId).HasColumnName("cuentaId");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipoMovimiento");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valor");

            entity.HasOne(d => d.oCuenta).WithOne(p => p.Movimiento)
                .HasForeignKey<Movimiento>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movimientos__id__3F466844");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__persona__3213E83FD122E34C");

            entity.ToTable("persona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
