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

    public virtual DbSet<SecAction> SecActions { get; set; }

    public virtual DbSet<SecModule> SecModules { get; set; }

    public virtual DbSet<SecModuleAction> SecModuleActions { get; set; }

    public virtual DbSet<SecRole> SecRoles { get; set; }

    public virtual DbSet<SecRoleModuleAction> SecRoleModuleActions { get; set; }

    public virtual DbSet<SecUser> SecUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SecAction>(entity =>
        {
            entity.ToTable("Sec_Action");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecModule>(entity =>
        {
            entity.ToTable("Sec_Module");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecModuleAction>(entity =>
        {
            entity.ToTable("Sec_ModuleAction");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Action).WithMany(p => p.SecModuleActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleAction_Action");

            entity.HasOne(d => d.Module).WithMany(p => p.SecModuleActions)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModuleAction_Module");
        });

        modelBuilder.Entity<SecRole>(entity =>
        {
            entity.ToTable("Sec_Roles");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SecRoleModuleAction>(entity =>
        {
            entity.ToTable("Sec_RoleModuleAction");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ModuleAction).WithMany(p => p.SecRoleModuleActions)
                .HasForeignKey(d => d.ModuleActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sec_RoleModuleAction_Sec_ModuleAction");

            entity.HasOne(d => d.Role).WithMany(p => p.SecRoleModuleActions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sec_RoleModuleAction_Sec_Roles");
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
