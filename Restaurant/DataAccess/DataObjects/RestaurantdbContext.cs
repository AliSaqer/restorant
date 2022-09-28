using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant.DataAccess.DataObjects
{
    public partial class RestaurantdbContext : DbContext
    {
        public RestaurantdbContext()
        {
        }

        public RestaurantdbContext(DbContextOptions<RestaurantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerRestaurantmenu> CustomerRestaurantmenus { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Restaurantmenu> Restaurantmenus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=alisaqer@1994;database=restaurantdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(180)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName).HasMaxLength(180);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<CustomerRestaurantmenu>(entity =>
            {
                entity.ToTable("customer_restaurantmenu");

                entity.HasIndex(e => e.CustomerId, "customer_id");

                entity.HasIndex(e => new { e.RestaurantmenuId, e.CustomerId }, "restaurantmenu_id_idx");

                entity.Property(e => e.CustomerRestaurantmenuId).HasColumnName("customer_restaurantmenu_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.RestaurantmenuId).HasColumnName("restaurantmenu_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerRestaurantmenus)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("customer_id");

                entity.HasOne(d => d.Restaurantmenu)
                    .WithMany(p => p.CustomerRestaurantmenus)
                    .HasForeignKey(d => d.RestaurantmenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("restaurantmenu_id");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .HasMaxLength(180)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Restaurantmenu>(entity =>
            {
                entity.ToTable("restaurantmenu");

                entity.HasIndex(e => e.RestaurantId, "Restaurant_id_idx");

                entity.HasIndex(e => e.Id, "idRestaurantMenu_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MealName).HasMaxLength(180);

                entity.Property(e => e.RestaurantId).HasColumnName("Restaurant_id");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Restaurantmenus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Restaurant_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
