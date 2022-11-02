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

  
    public virtual DbSet<SecRole> SecRoles { get; set; }
 
    public virtual DbSet<SecUser> SecUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 

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
