using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StorefrontDL.Entities
{
    public partial class P0DBContext : DbContext
    {
        public P0DBContext()
        {
        }

        public P0DBContext(DbContextOptions<P0DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<LineItem> LineItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Storefront> Storefronts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:jhe.database.windows.net,1433;Initial Catalog=P0 DB;Persist Security Info=False;User ID=asapjules;Password=Atomicbomb1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__LineItem__7D4CA69B7808082F");

                entity.ToTable("LineItem");

                entity.Property(e => e.ListId).HasColumnName("listID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.ProdId).HasColumnName("prodID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreId).HasColumnName("storeID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("foreign_key_order");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.ProdId)
                    .HasConstraintName("foreign_key_product");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("foreign_key_storelineitem");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("customerID");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Totalprice).HasColumnName("totalprice");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("foreign_key_customer");

                entity.HasOne(d => d.LocationNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Location)
                    .HasConstraintName("foreign_key_store");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Category)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Storefront>(entity =>
            {
                entity.ToTable("Storefront");

                entity.Property(e => e.Address)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
