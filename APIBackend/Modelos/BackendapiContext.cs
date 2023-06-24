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

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__clientes__47E34D649227631C");

            entity.ToTable("clientes");

            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");

            entity.HasOne(d => d.Persona).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__clientes__person__440B1D61");
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.CuentaId).HasName("PK__cuentas__612B08612A9CE06C");

            entity.ToTable("cuentas");

            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_cuenta");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo_inicial");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_cuenta");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__cuentas__cliente__46E78A0C");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("PK__movimien__A87EF0E52BE97771");

            entity.ToTable("movimientos");

            entity.Property(e => e.MovimientoId).HasColumnName("movimiento_id");
            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.FechaMovimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.SaldoDisponible)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("saldo_disponible");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_movimiento");
            entity.Property(e => e.ValorMovimiento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_movimiento");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK__movimient__cuent__49C3F6B7");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK__persona__189F813A49F16470");

            entity.ToTable("persona");

            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
