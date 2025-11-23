using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LAB4_Bryan_Segovia.Models;

public partial class DbContextTienda : DbContext
{
    public DbContextTienda()
    {
    }

    public DbContextTienda(DbContextOptions<DbContextTienda> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Detallesorden> Detallesordens { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=tiendabs;Username=postgres;Password=root123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Detallesorden>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("detallesorden_pkey");

            entity.ToTable("detallesorden");

            entity.HasIndex(e => e.OrdenId, "idx_detallesorden_orden_id");

            entity.HasIndex(e => e.ProductoId, "idx_detallesorden_producto_id");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.Cantidad).HasDefaultValue(0);
            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Orden).WithMany(p => p.Detallesordens)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DetallesOrden_Ordenes");

            entity.HasOne(d => d.Producto).WithMany(p => p.Detallesordens)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DetallesOrden_Productos");
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("ordenes_pkey");

            entity.ToTable("ordenes");

            entity.HasIndex(e => e.ClienteId, "idx_ordenes_cliente_id");

            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.FechaOrden).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Ordenes_Clientes");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("pagos_pkey");

            entity.ToTable("pagos");

            entity.HasIndex(e => e.OrdenId, "idx_pagos_orden_id");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.FechaPago).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.MetodoPago).HasMaxLength(50);
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");
            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");

            entity.HasOne(d => d.Orden).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Pagos_Ordenes");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.HasIndex(e => e.CategoriaId, "idx_productos_categoria_id");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");
            entity.Property(e => e.Stock).HasDefaultValue(0);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Productos_Categorias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
