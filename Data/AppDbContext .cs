using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using JFjewelery.Models;
using JFjewelery.Models.Characteristics;


namespace JFjewelery.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        //Tables-general
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPaymentMethod> CustomerPaymentMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ChatSession> ChatSessions { get; set; }


        //Characteristic tables
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicStone> CharacteristicStones { get; set; }
        public DbSet<CharacteristicMetal> CharacteristicMetals { get; set; }
        public DbSet<Coating> Coating { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Metal> Metal { get; set; }
        public DbSet<Shape> Shape { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Stone> Stone { get; set; }
        public DbSet<JType> Type { get; set; }
        public DbSet<AppliesToEntity> AppliesToEntities { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Rings" },
                new Category { Id = 2, Name = "Earrings" },
                new Category { Id = 3, Name = "Necklaces" },
                new Category { Id = 4, Name = "Bracelets" },
                new Category { Id = 5, Name = "Pendants" },
                new Category { Id = 6, Name = "Brooches" },
                new Category { Id = 7, Name = "Chains" },
                new Category { Id = 8, Name = "Ear cuffs" },
                new Category { Id = 9, Name = "Hair accessories" },
                new Category { Id = 10, Name = "Chokers" },
                new Category { Id = 11, Name = "Pins" }
                );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product { Id = 1, Name = "Ruby rose", CategoryId = 2, Quantity = 10 }
            //    );

            //modelBuilder.Entity<ProductImage>().HasData(
            //    new ProductImage { Id = 1, ProductId = 1, FileName = "prod_1_front_1.jpg" }
            //    );

            //modelBuilder.Entity<Characteristic>().HasData(
            //    new Characteristic { Id=1, ProductId=1, Material="Gold", Purity= 585, Weight=10, MetalColor="Yellow",
            //    Stone="Ruby", StoneCount=4, StoneShape="Pear", Size="Universal", Gender="Women", Style="Classic"}
            //    );

            //modelBuilder.Entity<PaymentMethod>().HasData(
            //    new PaymentMethod { Id=1, Name="Card"},
            //    new PaymentMethod { Id=2, Name="Paypal"},
            //    new PaymentMethod { Id=3, Name="Blik"}
            //    );

            //modelBuilder.Entity<AppliesToEntity>().HasData(
            //    new AppliesToEntity { Id = 1, Name = "Stone"},
            //    new AppliesToEntity { Id = 2, Name = "Metal" }
            //    );
            //modelBuilder.Entity<Color>().HasData(
            //    new Color { Id = 1, Name = "Red", AppliesToEntityId = 1 },
                
            //    );




        }
    }            
}
