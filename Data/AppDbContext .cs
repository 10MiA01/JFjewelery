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

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id=1, Name="Card"},
                new PaymentMethod { Id=2, Name="Paypal"},
                new PaymentMethod { Id=3, Name="Blik"}
                );

            modelBuilder.Entity<Metal>().HasData(
                new Metal { Id = 1, Name = "Gold" },
                new Metal { Id = 2, Name = "Silver" },
                new Metal { Id = 3, Name = "Platinum" },
                new Metal { Id = 4, Name = "Titanium" }
            );

            modelBuilder.Entity<Stone>().HasData(
                new Stone { Id = 1, Name = "Diamond" },
                new Stone { Id = 2, Name = "Ruby" },
                new Stone { Id = 3, Name = "Sapphire" },
                new Stone { Id = 4, Name = "Emerald" },
                new Stone { Id = 5, Name = "Amethyst" },
                new Stone { Id = 6, Name = "Topaz" },
                new Stone { Id = 7, Name = "Spinel" }
            );

            modelBuilder.Entity<AppliesToEntity>().HasData(
                new AppliesToEntity { Id = 1, Name = "Metal"},
                new AppliesToEntity { Id = 2, Name = "Stone" }
                );

            modelBuilder.Entity<Color>().HasData(
                // Metals (AppliesToEntityId = 1)
                new Color { Id = 1, Name = "Silver", AppliesToEntityId = 1 },
                new Color { Id = 2, Name = "Gold", AppliesToEntityId = 1 },
                new Color { Id = 3, Name = "Rose Gold", AppliesToEntityId = 1 },
                new Color { Id = 4, Name = "White Gold", AppliesToEntityId = 1 },
                new Color { Id = 5, Name = "Platinum", AppliesToEntityId = 1 },
                new Color { Id = 6, Name = "Black", AppliesToEntityId = 1 },

                // Stones (AppliesToEntityId = 2)
                new Color { Id = 7, Name = "Red", AppliesToEntityId = 2 },
                new Color { Id = 8, Name = "Blue", AppliesToEntityId = 2 },
                new Color { Id = 9, Name = "Green", AppliesToEntityId = 2 },
                new Color { Id = 10, Name = "Yellow", AppliesToEntityId = 2 },
                new Color { Id = 11, Name = "Pink", AppliesToEntityId = 2 },
                new Color { Id = 12, Name = "Purple", AppliesToEntityId = 2 },
                new Color { Id = 13, Name = "Orange", AppliesToEntityId = 2 },
                new Color { Id = 14, Name = "White", AppliesToEntityId = 2 },
                new Color { Id = 15, Name = "Black", AppliesToEntityId = 2 }
            );

            modelBuilder.Entity<Coating>().HasData(
                // Metals (AppliesToEntityId = 1)
                new Coating { Id = 1, Name = "Rhodium Plating", AppliesToEntityId = 1 },
                new Coating { Id = 2, Name = "Gold Plating", AppliesToEntityId = 1 },
                new Coating { Id = 3, Name = "Black Rhodium", AppliesToEntityId = 1 },
                new Coating { Id = 4, Name = "Silver Plating", AppliesToEntityId = 1 },
                new Coating { Id = 5, Name = "PVD Coating", AppliesToEntityId = 1 },

                // Stones (AppliesToEntityId = 2)
                new Coating { Id = 6, Name = "Irradiation", AppliesToEntityId = 2 },
                new Coating { Id = 7, Name = "Heat Treatment", AppliesToEntityId = 2 },
                new Coating { Id = 8, Name = "Dyeing", AppliesToEntityId = 2 },
                new Coating { Id = 9, Name = "Coating with Film", AppliesToEntityId = 2 }
            );

            modelBuilder.Entity<Shape>().HasData(
                // Metals (AppliesToEntityId = 1)
                new Shape { Id = 1, Name = "Round", AppliesToEntityId = 1 },
                new Shape { Id = 2, Name = "Square", AppliesToEntityId = 1 },
                new Shape { Id = 3, Name = "Oval", AppliesToEntityId = 1 },
                new Shape { Id = 4, Name = "Rectangle", AppliesToEntityId = 1 },
                new Shape { Id = 5, Name = "Triangle", AppliesToEntityId = 1 },

                // Stones (AppliesToEntityId = 2)
                new Shape { Id = 6, Name = "Brilliant Cut", AppliesToEntityId = 2 },
                new Shape { Id = 7, Name = "Princess Cut", AppliesToEntityId = 2 },
                new Shape { Id = 8, Name = "Cushion Cut", AppliesToEntityId = 2 },
                new Shape { Id = 9, Name = "Emerald Cut", AppliesToEntityId = 2 },
                new Shape { Id = 10, Name = "Marquise Cut", AppliesToEntityId = 2 },
                new Shape { Id = 11, Name = "Oval", AppliesToEntityId = 2 },
                new Shape { Id = 12, Name = "Round", AppliesToEntityId = 2 }
            );

            modelBuilder.Entity<JType>().HasData(
                // Metals (AppliesToEntityId = 1)
                new JType { Id = 1, Name = "Gold", AppliesToEntityId = 1 },
                new JType { Id = 2, Name = "Silver", AppliesToEntityId = 1 },
                new JType { Id = 3, Name = "Platinum", AppliesToEntityId = 1 },
                new JType { Id = 4, Name = "Palladium", AppliesToEntityId = 1 },
                new JType { Id = 5, Name = "Titanium", AppliesToEntityId = 1 },

                // Stones (AppliesToEntityId = 2)
                new JType { Id = 6, Name = "Star-shaped", AppliesToEntityId = 2 },
                new JType { Id = 7, Name = "Milky", AppliesToEntityId = 2 },
                new JType { Id = 8, Name = "Impure", AppliesToEntityId = 2 },
                new JType { Id = 9, Name = "Pure", AppliesToEntityId = 2 },
                new JType { Id = 10, Name = "Translucent", AppliesToEntityId = 2 }
            );

            modelBuilder.Entity<Size>().HasData(
                // Metal
                new Size { Id = 1, Name = "Small", AppliesToEntityId = 1 },
                new Size { Id = 2, Name = "Medium", AppliesToEntityId = 1 },
                new Size { Id = 3, Name = "Large", AppliesToEntityId = 1 },

                // Stone
                new Size { Id = 4, Name = "Tiny", AppliesToEntityId = 2 },
                new Size { Id = 5, Name = "Regular", AppliesToEntityId = 2 },
                new Size { Id = 6, Name = "Big", AppliesToEntityId = 2 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Ruby rose", CategoryId = 2, Quantity = 10, Price=5000 }
                );

            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 1, ProductId = 1, FileName = "prod_1_front_1.jpg" }
                );

            modelBuilder.Entity<Characteristic>().HasData(
                new Characteristic
                {
                    Id = 1,
                    ProductId = 1,
                    Gender = "Women",
                    Style = "Classic",
                    Manufacturer = "JFjewelery"
                }
            );

            modelBuilder.Entity<CharacteristicMetal>().HasData(
                new CharacteristicMetal
                {
                    Id = 1,
                    CharacteristicId = 1,
                    MetalId = 1,        // Gold
                    ColorId = 1,        // Yellow
                    ShapeId = 3,        // Oval
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 10f
                }
            );

            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 1,
                    CharacteristicId = 1,
                    StoneId = 2,       // Ruby
                    ShapeId = 11,       // Oval
                    ColorId = 7,       // Red
                    SizeId = 5,        // Regular (stone)
                    Count = 4
                }
            );

            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 2,
                    CharacteristicId = 1,
                    StoneId = 7,       // Spinel
                    ShapeId = 12,       // Round
                    ColorId = 14,       // Whit
                    TypeId = 9,         //Pure
                    SizeId = 5,        // Tiny (stone)
                    Count = 62
                }
            );









        }
    }            
}
