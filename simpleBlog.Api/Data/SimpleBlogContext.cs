using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using simpleBlog.Api.Interfaces;

#nullable disable

namespace simpleBlog.Api.data
{
    public partial class SimpleBlogContext : DbContext
    {
        IAppSettings appSettings;
        public SimpleBlogContext()
        {
        }

        public SimpleBlogContext(DbContextOptions<SimpleBlogContext> options, IAppSettings AppSettings)
            : base(options)
        {
            appSettings = AppSettings;
        }

        public virtual DbSet<Artikel> Artikels { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(appSettings.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "English_Indonesia.1252");

            modelBuilder.Entity<Artikel>(entity =>
            {
                entity.ToTable("artikel");

                entity.HasIndex(e => e.AuthorId, "fki_fk_artikel_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(6L, null, null, null, null, null);

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("created_date");

                entity.Property(e => e.Excerpt).HasColumnName("excerpt");

                entity.Property(e => e.PubDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("pub_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("updated_date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Artikels)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("fk_artikel_status");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Artikels)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("fk_artikel_user");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.RoleDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("role_description");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("role_name");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nama");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updated_by");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");

                entity.HasIndex(e => e.UserId, "fki_userrole_fkUserid");

                entity.HasIndex(e => e.RoleId, "fki_userrole_fkroleid");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userrole_fkroleid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userrole_fkUserid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
