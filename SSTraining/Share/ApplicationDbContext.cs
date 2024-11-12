using Microsoft.EntityFrameworkCore;
using SSTraining.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SSTraining.Share
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> Order_Product { get; set; }
        public DbSet<ShippingProvider> Shipping_Provider { get; set; }
        public DbSet<PaymentMethod> Payment_Method { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Ignore(sc => sc.CreatedAt)
                .Ignore(sc => sc.UpdatedAt);
            modelBuilder.Entity<Order>()
                .Property(o => o.DeliveryStatus)
                    .HasConversion(
                        v => v.ToString(),
                        v => (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), v));
            modelBuilder.Entity<Order>()
                   .Property(o => o.PaymentStatus)
                   .HasConversion(
                       v => v.ToString(),
                       v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));


            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(op => op.Id);

                entity.HasOne(op => op.Order)
                      .WithMany(o => o.OrderProducts)
                      .HasForeignKey(op => op.Order_Id);

                entity.HasOne(op => op.Product)
                      .WithMany(p => p.OrderProducts)
                      .HasForeignKey(op => op.Product_Id);
            });


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CreatedAt).HasColumnName("Created_At");
                entity.Property(c => c.UpdatedAt).HasColumnName("Updated_At");
                entity.Property(c => c.TotalAmount).HasColumnName("Total_Amount");
                entity.Property(c => c.CustomerId).HasColumnName("Customer_Id");
            });
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.ShoppingCarts)
                .WithOne(sc => sc.Cart)
                .HasForeignKey(sc => sc.Cart_Id)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ShoppingCart>()
                .HasKey(sc =>  sc.Id);
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Cart)
                .WithMany(c => c.ShoppingCarts)
                .HasForeignKey(sc => sc.Cart_Id);
            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Product)
                .WithMany(p => p.ShoppingCarts)
                .HasForeignKey(sc => sc.Product_Id);
            modelBuilder.Entity<ShoppingCart>()
                .Ignore(sc => sc.Price);

            base.OnModelCreating(modelBuilder);
        }

    }
}
