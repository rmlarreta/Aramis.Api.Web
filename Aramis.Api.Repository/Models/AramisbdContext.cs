using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Aramis.Api.Repository.Models;

public partial class AramisbdContext : DbContext
{
    public AramisbdContext()
    {
    }

    public AramisbdContext(DbContextOptions<AramisbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OpCliente> OpClientes { get; set; }

    public virtual DbSet<OpGender> OpGenders { get; set; }

    public virtual DbSet<OpPai> OpPais { get; set; }

    public virtual DbSet<OpResp> OpResps { get; set; }

    public virtual DbSet<SecRole> SecRoles { get; set; }

    public virtual DbSet<SecUser> SecUsers { get; set; }     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OpCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Op_Clientes_Id");

            entity.ToTable("Op_Clientes");

            entity.HasIndex(e => e.Cui, "KEY_Op_Clientes_Cui").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Contacto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cui)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Domicilio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Razon)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.OpClientes)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Op_Clientes_Gender");

            entity.HasOne(d => d.PaisNavigation).WithMany(p => p.OpClientes)
                .HasForeignKey(d => d.Pais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Op_Clientes_Pais");

            entity.HasOne(d => d.RespNavigation).WithMany(p => p.OpClientes)
                .HasForeignKey(d => d.Resp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Op_Clientes_Resp");
        });

        modelBuilder.Entity<OpGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Op_Gender_Id");

            entity.ToTable("Op_Gender");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OpPai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Op_Pais_Id");

            entity.ToTable("Op_Pais");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OpResp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Op_Resp_Id");

            entity.ToTable("Op_Resp");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecRole>(entity =>
        {
            entity.ToTable("Sec_Roles");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecUser>(entity =>
        {
            entity.ToTable("Sec_Users");

            entity.HasIndex(e => e.UserName, "KEY_Sec_Users_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndOfLife).HasColumnType("datetime");
            entity.Property(e => e.RealName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.SecUsers)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sec_Users_Sec_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
