using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PreSchoolApp.Models.Entities
{
    public partial class PreSchoolAppContext : DbContext
    {
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<C2p> C2p { get; set; }
        public virtual DbSet<Children> Children { get; set; }
        public virtual DbSet<PreSchools> PreSchools { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:preschoolserver.database.windows.net,1433;Initial Catalog=PreSchoolDB;Persist Security Info=False;User ID=preschoolAdmin;Password=Grupp1C#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<C2p>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.Cid });

                entity.ToTable("C2P", "PRS");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.Property(e => e.Cid).HasColumnName("CID");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.C2p)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__C2P__CID__7B5B524B");

                entity.HasOne(d => d.U)
                    .WithMany(p => p.C2p)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__C2P__UID__7A672E12");
            });

            modelBuilder.Entity<Children>(entity =>
            {
                entity.ToTable("Children", "PRS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.UnitsId).HasColumnName("UnitsID");

                entity.HasOne(d => d.Units)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.UnitsId)
                    .HasConstraintName("FK__Children__UnitsI__778AC167");
            });

            modelBuilder.Entity<PreSchools>(entity =>
            {
                entity.ToTable("PreSchools", "PRS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PreSchoolName).HasMaxLength(100);
            });

            modelBuilder.Entity<Schedules>(entity =>
            {
                entity.ToTable("Schedules", "PRS");

                entity.HasIndex(e => new { e.Weekdays, e.ChildrenId })
                    .HasName("UQ__Schedule__052ACB7EBA1D2EAC")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChildrenId).HasColumnName("ChildrenID");

                entity.Property(e => e.Weekdays)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.HasOne(d => d.Children)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ChildrenId)
                    .HasConstraintName("FK__Schedules__Child__7F2BE32F");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.ToTable("Units", "PRS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PreSchoolsId).HasColumnName("PreSchoolsID");

                entity.Property(e => e.UnitName).HasMaxLength(32);

                entity.HasOne(d => d.PreSchools)
                    .WithMany(p => p.Units)
                    .HasForeignKey(d => d.PreSchoolsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Units__PreSchool__6FE99F9F");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "PRS");

                entity.HasIndex(e => e.AspId)
                    .HasName("UQ__Users__446BE1C4C49C459C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AspId)
                    .IsRequired()
                    .HasColumnName("AspID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.UnitsId).HasColumnName("UnitsID");

                entity.HasOne(d => d.Asp)
                    .WithOne(p => p.Users)
                    .HasForeignKey<Users>(d => d.AspId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__AspID__73BA3083");

                entity.HasOne(d => d.Units)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UnitsId)
                    .HasConstraintName("FK__Users__UnitsID__74AE54BC");
            });
        }
    }
}
