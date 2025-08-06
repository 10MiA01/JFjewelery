using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using JFjewelery.Models;
using JFjewelery.Models.Characteristics;
using JFjewelery.Models.Scenario;
using System.Reflection.PortableExecutable;


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

        //Scenario tables
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Option> Options { get; set; }


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
                new Stone { Id = 7, Name = "Spinel" },
                new Stone { Id = 8, Name = "Pearl" },
                new Stone { Id = 9, Name = "obsydian" }
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
                new Shape { Id = 12, Name = "Round", AppliesToEntityId = 2 },
                new Shape { Id = 13, Name = "Rectangle", AppliesToEntityId = 2 }
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
                new Product { Id = 1, Name = "Ruby rose", CategoryId = 2, Quantity = 10, Price=5000 },
                new Product { Id = 2, Name = "Stained glass", CategoryId = 2, Quantity = 10, Price = 1000 },
                new Product { Id = 3, Name = "Pink treasure", CategoryId = 2, Quantity = 10, Price = 4000 },
                new Product { Id = 4, Name = "Lara's wish", CategoryId = 4, Quantity = 10, Price = 1500 },
                new Product { Id = 5, Name = "Sweet bird", CategoryId = 2, Quantity = 10, Price = 2000 },
                new Product { Id = 6, Name = "Marine rope", CategoryId = 2, Quantity = 10, Price = 1000 },
                new Product { Id = 7, Name = "Sea tear", CategoryId = 1, Quantity = 10, Price = 2000 },
                new Product { Id = 8, Name = "Demon's eye", CategoryId = 1, Quantity = 10, Price = 3000 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 9, Name = "Fox tail", CategoryId = 3, Quantity = 10, Price = 2000},
                new Product { Id = 10, Name = "Queen", CategoryId = 3, Quantity = 10, Price = 2500 }
                );

            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 1, ProductId = 1, FileName = "prod_1_front_1.jpg" },
                new ProductImage { Id = 2, ProductId = 2, FileName = "prod_2_front_1.jpg" },
                new ProductImage { Id = 3, ProductId = 3, FileName = "prod_3_front_1.jpg" },
                new ProductImage { Id = 4, ProductId = 4, FileName = "prod_4_front_1.jpg" },
                new ProductImage { Id = 5, ProductId = 5, FileName = "prod_5_front_1.jpg" },
                new ProductImage { Id = 6, ProductId = 6, FileName = "prod_6_front_1.jpg" },
                new ProductImage { Id = 7, ProductId = 7, FileName = "prod_7_front_1.jpg" },
                new ProductImage { Id = 8, ProductId = 8, FileName = "prod_8_front_1.jpg" }
                );
            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { Id = 9, ProductId = 9, FileName = "prod_9_front_1.jpg" },
                new ProductImage { Id = 10, ProductId = 10, FileName = "prod_10_front_1.jpg" }
                );

            modelBuilder.Entity<Characteristic>().HasData(
                new Characteristic
                {
                    Id = 1,
                    ProductId = 1,
                    Gender = "Women",
                    Style = "Classic",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 2,
                    ProductId = 2,
                    Gender = "Both",
                    Style = "Vintage",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 3,
                    ProductId = 3,
                    Gender = "Women",
                    Style = "Romantic",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 4,
                    ProductId = 4,
                    Gender = "Women",
                    Style = "Minimalistic",
                    Manufacturer = "JFjewelery"
                }, new Characteristic
                {
                    Id = 5,
                    ProductId = 5,
                    Gender = "Women",
                    Style = "Classic",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 6,
                    ProductId = 6,
                    Gender = "Women",
                    Style = "Minimalism",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 7,
                    ProductId = 7,
                    Gender = "Women",
                    Style = "Minimalism",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 8,
                    ProductId = 8,
                    Gender = "Men",
                    Style = "Vintage",
                    Manufacturer = "JFjewelery"
                }
            );

            modelBuilder.Entity<Characteristic>().HasData(
                new Characteristic
                {
                    Id = 9,
                    ProductId = 9,
                    Gender = "Women",
                    Style = "Romantic",
                    Manufacturer = "JFjewelery"
                },
                new Characteristic
                {
                    Id = 10,
                    ProductId = 10,
                    Gender = "Both",
                    Style = "Vintage",
                    Manufacturer = "JFjewelery"
                }
            );

            modelBuilder.Entity<CharacteristicMetal>().HasData(
                new CharacteristicMetal
                {
                    Id = 1,
                    CharacteristicId = 1,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    ShapeId = 3,        // Oval
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 25
                },
                new CharacteristicMetal
                {
                    Id = 2,
                    CharacteristicId = 2,
                    MetalId = 2,        // Silver
                    ColorId = 1,        // Silver
                    SizeId = 2,         // Medium
                    TypeId = 2,         // Silver
                    Purity = 585,
                    Weight = 10
                },
                new CharacteristicMetal
                {
                    Id = 3,
                    CharacteristicId = 3,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 1,         // Small
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 10
                },
                new CharacteristicMetal
                {
                    Id = 4,
                    CharacteristicId = 4,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 20
                },
                new CharacteristicMetal
                {
                    Id = 5,
                    CharacteristicId = 5,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    ShapeId = 2,        // Square
                    SizeId = 3,         // Large
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 50
                },
                new CharacteristicMetal
                {
                    Id = 6,
                    CharacteristicId = 6,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 15
                },
                new CharacteristicMetal
                {
                    Id = 7,
                    CharacteristicId = 7,
                    MetalId = 2,        // Silver
                    ColorId = 1,        // Silver
                    SizeId = 2,         // Medium
                    TypeId = 2,         // Silver
                    Purity = 585,
                    Weight = 10
                },
                new CharacteristicMetal
                {
                    Id = 8,
                    CharacteristicId = 8,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 20
                }
            );
            modelBuilder.Entity<CharacteristicMetal>().HasData(
                new CharacteristicMetal
                {
                    Id = 9,
                    CharacteristicId = 9,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 15
                },
                new CharacteristicMetal
                {
                    Id = 10,
                    CharacteristicId = 10,
                    MetalId = 1,        // Gold
                    ColorId = 2,        // Gold
                    SizeId = 2,         // Medium
                    TypeId = 1,         // Gold
                    Purity = 585,
                    Weight = 20
                }
            );

            //Product 1
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

            //Product 1
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 2,
                    CharacteristicId = 1,
                    StoneId = 7,       // Spinel
                    ShapeId = 12,       // Round
                    ColorId = 14,       // White
                    TypeId = 9,         //Pure
                    SizeId = 4,        // Tiny (stone)
                    Count = 62
                }
            );
            
            //Product 2
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 3,
                    CharacteristicId = 2,
                    StoneId = 1,       
                    ShapeId = 13,       // Rectangle
                    ColorId = 14,       // Whit
                    TypeId = 9,         //Pure
                    SizeId = 4,        // Tiny (stone)
                    Count = 4
                },
                new CharacteristicStone
                {
                    Id = 4,
                    CharacteristicId = 2,
                    StoneId = 2,
                    ShapeId = 13,       // Rectangle
                    ColorId = 7,       // Red
                    TypeId = 9,         //Pure
                    SizeId = 4,        // Tiny (stone)
                    Count = 4
                },
                new CharacteristicStone
                {
                    Id = 5,
                    CharacteristicId = 2,
                    StoneId = 3,
                    ShapeId = 13,       // Rectangle
                    ColorId = 8,       // Blue
                    TypeId = 9,         //Pure
                    SizeId = 5,        // Tiny (stone)
                    Count = 4
                },
                new CharacteristicStone
                {
                    Id = 6,
                    CharacteristicId = 2,
                    StoneId = 5,
                    ShapeId = 13,       // Rectangle
                    ColorId = 12,       // Purple
                    TypeId = 9,         //Pure
                    SizeId = 5,        // Tiny (stone)
                    Count = 4
                }
            );

            //Product 3
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 7,
                    CharacteristicId = 3,
                    StoneId = 7,       // Spinel
                    ShapeId = 12,       // Round
                    ColorId = 11,       // Pink
                    TypeId = 9,         //Pure
                    SizeId = 4,        // Tiny (stone)
                    Count = 8
                }
            );

            //Product 4
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 8,
                    CharacteristicId = 4,
                    StoneId = 1,       // Diamond
                    ShapeId = 12,       // Round
                    ColorId = 14,       // White
                    TypeId = 10,         //Translucent
                    SizeId = 4,        // Tiny (stone)
                    Count = 5
                }
            );

            //Product 5
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 9,
                    CharacteristicId = 5,
                    StoneId = 3,       // Saphire
                    ShapeId = 13,       // Rectangle
                    ColorId = 11,       // Pink
                    TypeId = 9,         //Pure
                    SizeId = 6,        // Big (stone)
                    Count = 2
                },
                new CharacteristicStone
                {
                    Id = 10,
                    CharacteristicId = 5,
                    StoneId = 7,       // Spinel
                    ShapeId = 13,       // Rectangle
                    ColorId = 14,       // White
                    TypeId = 10,         //Translucent
                    SizeId = 5,        // Regular (stone)
                    Count = 2
                }
            );

            //Product 7
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 11,
                    CharacteristicId = 7,
                    StoneId = 8,       // Pearl
                    ShapeId = 12,       // Round
                    ColorId = 14,       // White
                    TypeId = 7,         //Milky
                    SizeId = 5,        // Regular (stone)
                    Count = 1
                },
                new CharacteristicStone
                {
                    Id = 12,
                    CharacteristicId = 7,
                    StoneId = 1,       // Diamond
                    ShapeId = 12,       // Round
                    ColorId = 14,       // White
                    TypeId = 10,         //Translucent
                    SizeId = 4,        // Tiny (stone)
                    Count = 1
                }
            );

            //Product 8
            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 13,
                    CharacteristicId = 8,
                    StoneId = 2,       // Ruby
                    ShapeId = 12,       // Round
                    ColorId = 7,       // Red
                    TypeId = 9,         //Pure
                    SizeId = 5,        // Regular (stone)
                    Count = 1
                },
                new CharacteristicStone
                {
                    Id = 14,
                    CharacteristicId = 8,
                    StoneId = 9,       // Obsidian
                    ShapeId = 12,       // Round
                    ColorId = 15,       // Black
                    SizeId = 4,        // Tiny (stone)
                    Count = 12
                }
            );

            modelBuilder.Entity<CharacteristicStone>().HasData(
                new CharacteristicStone
                {
                    Id = 15,
                    CharacteristicId = 10,
                    StoneId = 7,       // Spinel
                    ShapeId = 12,       // Round
                    ColorId = 14,       // White
                    TypeId = 9,         //Pure
                    SizeId = 5,        // Regular (stone)
                    Count = 1
                }
            );

            modelBuilder.Entity<Scenario>().HasData(
                new Scenario { Id = 1, Name = "Personal form" },
                new Scenario { Id = 2, Name = "Custom for an event" },
                new Scenario { Id = 3, Name = "Custom characteristics"}, 
                new Scenario { Id = 4, Name = "Custom by picture" },
                new Scenario { Id = 5, Name = "Virtual fitting" }
            );

            //Scenario 1, steps
            modelBuilder.Entity<Step>().HasData(
                new Step 
                {
                    Id = 1, 
                    Name="Stone", 
                    QuestionText = "If you were a stone, what kind would you be?",
                    NextStepId = 2,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 2,
                    Name = "Season",
                    QuestionText = "What is your favorite season?",
                    NextStepId = 3,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 3,
                    Name = "Archetype",
                    QuestionText = "Which archetype best represents you?",
                    NextStepId = 4,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 4,
                    Name = "Music",
                    QuestionText = "If your ideal piece of jewelry were music, what would it sound like?",
                    NextStepId = 5,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 5,
                    Name = "Scent",
                    QuestionText = "If your ideal piece of jewelry had a scent, what would it be?",
                    NextStepId = 6,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 6,
                    Name = "Energy",
                    QuestionText = "What kind of energy would you like your jewelry to amplify",
                    NextStepId = 7,
                    ScenarioId = 1,
                },
                new Step
                {
                    Id = 7,
                    Name = "Power",
                    QuestionText = "If your jewelry had a hidden power, what would it help you with?",
                    ScenarioId = 1,
                }

             );

            //Scenario 1, step 1, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 1,
                    Name = "Diamond",
                    Content = "Timeless, brilliant, and strong.",
                    FilterJson = "{\"Stones\":[\"Diamond\"],\"StoneColors\":[\"White\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Round\",\"Princess\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"CountMin\":1}",
                    StepId = 1
                },
                new Option
                {
                    Id = 2,
                    Name = "Amethyst",
                    Content = "Mysterious, spiritual, and creative.",
                    FilterJson = "{\"Stones\":[\"Amethyst\"],\"StoneColors\":[\"Purple\"],\"StoneTypes\":[\"Semi-Precious\"],\"StoneShapes\":[\"Oval\",\"Marquise\"],\"StoneSizes\":[\"Medium\"],\"CountMin\":1}",
                    StepId = 1
                },
                new Option
                {
                    Id = 3,
                    Name = "Emerald",
                    Content = "Natural, elegant, and deeply intuitive.",
                    FilterJson = "{\"Stones\":[\"Emerald\"],\"StoneColors\":[\"Green\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Emerald\",\"Cushion\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"CountMin\":1}",
                    StepId = 1
                },
                new Option
                {
                    Id = 4,
                    Name = "Ruby",
                    Content = "Passionate, bold, and full of energy.",
                    FilterJson = "{\"Stones\":[\"Ruby\"],\"StoneColors\":[\"Red\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Small\",\"Medium\"],\"CountMin\":1}",
                    StepId = 1
                },
                new Option
                {
                    Id = 5,
                    Name = "Sapphire",
                    Content = "Calm, wise, and emotionally deep.",
                    FilterJson = "{\"Stones\":[\"Sapphire\"],\"StoneColors\":[\"Blue\"],\"StoneTypes\":[\"Precious\"],\"StoneShapes\":[\"Round\",\"Cushion\"],\"StoneSizes\":[\"Medium\"],\"CountMin\":1}",
                    StepId = 1
                },
                new Option
                {
                    Id = 6,
                    Name = "Obsidian",
                    Content = "Grounded, edgy, and protective.",
                    FilterJson = "{\"Stones\":[\"Obsidian\"],\"StoneColors\":[\"Black\"],\"StoneTypes\":[\"Organic\"],\"StoneShapes\":[\"Oval\",\"Cabochon\"],\"StoneSizes\":[\"Large\"],\"CountMin\":1}",
                    StepId = 1
                }
             );

            //Scenario 1, step 2, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 7,
                    Name = "Spring",
                    Content = "Fresh, blooming, and full of life.",
                    FilterJson = "{\"Styles\":[\"Floral\",\"Nature\"],\"MetalColors\":[\"Rose Gold\"],\"StoneColors\":[\"Pink\",\"Green\"],\"Stones\":[\"Peridot\",\"Rose Quartz\"],\"StoneTypes\":[\"Semi-Precious\"]}",
                    StepId = 2
                },
                new Option
                {
                    Id = 8,
                    Name = "Summer",
                    Content = "Bright, vibrant, and radiant.",
                    FilterJson = "{\"Styles\":[\"Bold\",\"Tropical\"],\"MetalColors\":[\"Yellow Gold\"],\"Stones\":[\"Topaz\",\"Citrine\"],\"StoneColors\":[\"Yellow\",\"Light Blue\"],\"MetalTypes\":[\"Polished\"]}",
                    StepId = 2
                },
                new Option
                {
                    Id = 9,
                    Name = "Autumn",
                    Content = "Warm, rich, and earthy.",
                    FilterJson = "{\"Styles\":[\"Vintage\",\"Boho\"],\"MetalColors\":[\"Copper\",\"Bronze\"],\"Stones\":[\"Garnet\",\"Tiger's Eye\"],\"StoneColors\":[\"Brown\",\"Red\",\"Orange\"],\"MetalTypes\":[\"Matte\"]}",
                    StepId = 2
                },
                new Option
                {
                    Id = 10,
                    Name = "Winter",
                    Content = "Cool, calm, and sparkling.",
                    FilterJson = "{\"Styles\":[\"Minimalist\",\"Classic\"],\"MetalColors\":[\"White Gold\",\"Silver\"],\"Stones\":[\"Diamond\",\"Sapphire\"],\"StoneColors\":[\"White\",\"Blue\"],\"StoneTypes\":[\"Precious\"],\"Purity\":925}",
                    StepId = 2
                }
            );

            //Scenario 1, step 3, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 11,
                    Name = "The Sage",
                    Content = "Seeker of truth and wisdom.",
                    FilterJson = "{\"Styles\":[\"Classic\",\"Minimalist\"],\"MetalTypes\":[\"Brushed\"],\"Stones\":[\"Sapphire\"],\"StoneColors\":[\"Blue\"],\"StoneTypes\":[\"Precious\"],\"Purity\":925}",
                    StepId = 3
                },
                new Option
                {
                    Id = 12,
                    Name = "🛡The Warrior",
                    Content = "Driven, focused, protective.",
                    FilterJson = "{\"Styles\":[\"Bold\",\"Military\"],\"MetalTypes\":[\"Matte\"],\"Metals\":[\"Steel\",\"Titanium\"],\"WeightMin\":10,\"StoneTypes\":[\"None\"]}",
                    StepId = 3
                },
                new Option
                {
                    Id = 13,
                    Name = "The Explorer",
                    Content = "Adventurous and independent.",
                    FilterJson = "{\"Styles\":[\"Boho\",\"Rustic\"],\"Metals\":[\"Silver\"],\"StoneShapes\":[\"Raw\"],\"StoneColors\":[\"Green\",\"Brown\"],\"Stones\":[\"Tourmaline\",\"Agate\"]}",
                    StepId = 3
                },
                new Option
                {
                    Id = 14,
                    Name = "The Ruler",
                    Content = "A natural leader and organizer.",
                    FilterJson = "{\"Styles\":[\"Luxury\",\"Formal\"],\"Metals\":[\"Gold\"],\"MetalColors\":[\"Yellow Gold\"],\"Stones\":[\"Diamond\",\"Ruby\"],\"StoneTypes\":[\"Precious\"],\"Purity\":750}",
                    StepId = 3
                },
                new Option
                {
                    Id = 15,
                    Name = "The Creator",
                    Content = "Imaginative, artistic, visionary.",
                    FilterJson = "{\"Styles\":[\"Artistic\",\"Abstract\"],\"MetalColors\":[\"Mixed\"],\"Stones\":[\"Opal\",\"Amethyst\"],\"StoneColors\":[\"Violet\",\"Iridescent\"],\"StoneTypes\":[\"Semi-Precious\"]}",
                    StepId = 3
                },
                new Option
                {
                    Id = 16,
                    Name = "The Magician",
                    Content = "Insightful, transformative, intuitive.",
                    FilterJson = "{\"Styles\":[\"Mystic\",\"Elegant\"],\"Stones\":[\"Moonstone\",\"Labradorite\"],\"StoneColors\":[\"Grey\",\"Blue\"],\"MetalTypes\":[\"Oxidized\"],\"Description\":\"Spiritual focus\"}",
                    StepId = 3
                }
            );

            //Scenario 1, step 4, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 17,
                    Name = "Smooth Jazz",
                    Content = "Soulful, intimate, full of depth.",
                    FilterJson = "{\"Styles\":[\"Romantic\",\"Elegant\"],\"Metals\":[\"Rose Gold\"],\"StoneColors\":[\"Deep Blue\",\"Purple\"],\"StoneTypes\":[\"Semi-Precious\"]}",
                    StepId = 4
                },
                new Option
                {
                    Id = 18,
                    Name = "🎛Electronic Beats",
                    Content = "Modern, vibrant, full of energy.",
                    FilterJson = "{\"Styles\":[\"Modern\",\"Edgy\"],\"MetalColors\":[\"Black\",\"Chrome\"],\"Metals\":[\"Titanium\"],\"Stones\":[\"Cubic Zirconia\"],\"StoneColors\":[\"Neon\"]}",
                    StepId = 4
                },
                new Option
                {
                    Id = 19,
                    Name = "Classical Symphony",
                    Content = "Elegant, timeless, structured.",
                    FilterJson = "{\"Styles\":[\"Classic\"],\"Metals\":[\"Gold\"],\"StoneTypes\":[\"Precious\"],\"Stones\":[\"Diamond\",\"Pearl\"],\"MetalTypes\":[\"Polished\"]}",
                    StepId = 4
                },
                new Option
                {
                    Id = 20,
                    Name = "Indie Acoustic",
                    Content = "Gentle, personal, a bit nostalgic.",
                    FilterJson = "{\"Styles\":[\"Vintage\",\"Rustic\"],\"Stones\":[\"Amber\",\"Moonstone\"],\"MetalColors\":[\"Copper\",\"Bronze\"],\"StoneColors\":[\"Soft White\",\"Honey\"]}",
                    StepId = 4
                },
                new Option
                {
                    Id = 21,
                    Name = "Ambient Silence",
                    Content = "Minimalist, calming, introspective.",
                    FilterJson = "{\"Styles\":[\"Minimalist\"],\"Metals\":[\"Silver\"],\"StoneTypes\":[],\"MetalTypes\":[\"Matte\"],\"WeightMax\":5}",
                    StepId = 4
                },
                new Option
                {
                    Id = 22,
                    Name = "World Fusion",
                    Content = "Eclectic, rich with cultural textures.",
                    FilterJson = "{\"Styles\":[\"Ethnic\",\"Boho\"],\"Stones\":[\"Turquoise\",\"Garnet\"],\"StoneColors\":[\"Red\",\"Green\",\"Blue\"],\"Metals\":[\"Mixed\"]}",
                    StepId = 4
                }
            );

            //Scenario 1, step 5, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 23,
                    Name = "Floral",
                    Content = "Soft, romantic, blooming with charm",
                    StepId = 5,
                    FilterJson = "{\"Styles\": [\"Romantic\"], \"StoneTypes\": [\"Semi-Precious\"], \"StoneColors\": [\"Pink\", \"Lavender\"]}"
                },
                new Option
                {
                    Id = 24,
                    Name = "Woody",
                    Content = "Earthy, grounded, with quiet strength",
                    StepId = 5,
                    FilterJson = "{\"MetalColors\": [\"Brown\", \"Copper\"], \"StoneShapes\": [\"Oval\", \"Cushion\"], \"StoneTypes\": [\"Natural\"]}"
                },
                new Option
                {
                    Id = 25,
                    Name = "Oriental",
                    Content = "Deep, spicy, mysterious",
                    StepId = 5,
                    FilterJson = "{\"StoneColors\": [\"Red\", \"Amber\"], \"MetalTypes\": [\"Engraved\"], \"Styles\": [\"Exotic\"]}"
                },
                new Option
                {
                    Id = 26,
                    Name = "Oceanic",
                    Content = "Fresh, clean, with a sense of freedom",
                    StepId = 5,
                    FilterJson = "{\"StoneColors\": [\"Blue\", \"Aqua\"], \"Styles\": [\"Marine\"], \"MetalColors\": [\"Silver\"]}"
                },
                new Option
                {
                    Id = 27,
                    Name = "Powdery",
                    Content = "Gentle, nostalgic, subtly elegant",
                    StepId = 5,
                    FilterJson = "{\"Styles\": [\"Vintage\"], \"StoneColors\": [\"Peach\", \"White\"], \"MetalShapes\": [\"Soft\"]}"
                },
                new Option
                {
                    Id = 28,
                    Name = "Unscented (Minimalist)",
                    Content = "Pure, simple, speaks without a trace",
                    StepId = 5,
                    FilterJson = "{\"Styles\": [\"Minimalist\"], \"StoneTypes\": [], \"MetalTypes\": [\"Plain\"], \"StoneColors\": [\"Clear\"]}"
                }
            );

            //Scenario 1, step 6, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 29,
                    Name = "Mystery",
                    Content = "Subtle, intriguing, layered",
                    StepId = 6,
                    FilterJson = "{\"StoneTypes\": [\"Obsidian\", \"Amethyst\"], \"StoneColors\": [\"Black\", \"Purple\"], \"Styles\": [\"Mystic\"]}"
                },
                new Option
                {
                    Id = 30,
                    Name = "Passion",
                    Content = "Bold, fiery, full of emotion",
                    StepId = 6,
                    FilterJson = "{\"StoneColors\": [\"Red\"], \"Stones\": [\"Ruby\"], \"Styles\": [\"Dramatic\"], \"MetalColors\": [\"Gold\"]}"
                },
                new Option
                {
                    Id = 31,
                    Name = "Lightness",
                    Content = "Airy, carefree, joyful",
                    StepId = 6,
                    FilterJson = "{\"StoneColors\": [\"Pink\", \"Sky Blue\"], \"MetalTypes\": [\"Thin\"], \"Styles\": [\"Playful\"]}"
                },
                new Option
                {
                    Id = 32,
                    Name = "Cool Elegance",
                    Content = "Refined, distant, composed",
                    StepId = 6,
                    FilterJson = "{\"StoneColors\": [\"White\", \"Blue\"], \"Styles\": [\"Elegant\"], \"MetalColors\": [\"Platinum\", \"Silver\"]}"
                },
                new Option
                {
                    Id = 33,
                    Name = "Playfulness",
                    Content = "Fun, whimsical, unexpected",
                    StepId = 6,
                    FilterJson = "{\"StoneShapes\": [\"Heart\", \"Star\"], \"Styles\": [\"Fun\"], \"StoneColors\": [\"Multicolor\"]}"
                },
                new Option
                {
                    Id = 34,
                    Name = "Confidence",
                    Content = "Strong, assertive, unmistakable",
                    StepId = 6,
                    FilterJson = "{\"MetalTypes\": [\"Bold\"], \"Stones\": [\"Diamond\"], \"Styles\": [\"Statement\"], \"StoneColors\": [\"Clear\"]}"
                }
            );


            //Scenario 1, step 7, options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 35,
                    Name = "Inner Clarity",
                    Content = "Helping you see your true path",
                    StepId = 7,
                    FilterJson = "{\"StoneTypes\": [\"Quartz\"], \"StoneColors\": [\"Clear\"], \"Styles\": [\"Minimalist\"], \"Description\": \"Clarity and insight\"}"
                },
                new Option
                {
                    Id = 36,
                    Name = "Courage",
                    Content = "Giving you strength to speak and act boldly",
                    StepId = 7,
                    FilterJson = "{\"Stones\": [\"Ruby\"], \"StoneColors\": [\"Red\"], \"Styles\": [\"Bold\"], \"Description\": \"Confidence and assertiveness\"}"
                },
                new Option
                {
                    Id = 37,
                    Name = "Protection",
                    Content = "Guarding your energy and intentions",
                    StepId = 7,
                    FilterJson = "{\"Stones\": [\"Obsidian\"], \"StoneColors\": [\"Black\"], \"StoneTypes\": [\"Protective\"], \"Styles\": [\"Mystic\"]}"
                },
                new Option
                {
                    Id = 38,
                    Name = "Love & Connection",
                    Content = "Attracting deep bonds and warmth",
                    StepId = 7,
                    FilterJson = "{\"StoneColors\": [\"Pink\"], \"Stones\": [\"Rose Quartz\"], \"Styles\": [\"Romantic\"], \"Description\": \"Emotional connection\"}"
                },
                new Option
                {
                    Id = 39,
                    Name = "Creativity",
                    Content = "Sparking ideas and artistic flow",
                    StepId = 7,
                    FilterJson = "{\"StoneColors\": [\"Purple\"], \"Stones\": [\"Amethyst\"], \"Styles\": [\"Artistic\"], \"Description\": \"Imagination and inspiration\"}"
                },
                new Option
                {
                    Id = 40,
                    Name = "Transformation",
                    Content = "Guiding you through change with grace",
                    StepId = 7,
                    FilterJson = "{\"StoneColors\": [\"Blue\", \"Green\"], \"Stones\": [\"Labradorite\"], \"Styles\": [\"Mystic\"], \"Description\": \"Change and growth\"}"
                }
            );

            //Scenario 2, steps
            modelBuilder.Entity<Step>().HasData(
                new Step
                {
                    Id = 8,
                    Name = "Occasion",
                    QuestionText = "What is the occasion?",
                    NextStepId = 9,
                    ScenarioId = 2,
                },
                new Step
                {
                    Id = 9,
                    Name = "Mood",
                    QuestionText = "What mood do you want to express",
                    NextStepId = 10,
                    ScenarioId = 2,
                },
                new Step
                {
                    Id = 10,
                    Name = "Clothing",
                    QuestionText = "What style of clothing do you plan to wear?",
                    NextStepId = 11,
                    ScenarioId = 2,
                },
                new Step
                {
                    Id = 11,
                    Name = "Metal",
                    QuestionText = "What metal do you prefer?",
                    NextStepId = 12,
                    ScenarioId = 2,
                },
                new Step
                {
                    Id = 12,
                    Name = "Type of jewelry",
                    QuestionText = "What type of jewelry are you interested in?",
                    ScenarioId = 2,
                }

             );

            //Scenario 2, step 1, options (Ocasion)
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 41,
                    Name = "Wedding",
                    Content = "Elegant and timeless styles for your special day.",
                    FilterJson = "{\"Styles\":[\"Classic\",\"Elegant\"],\"Metals\":[\"Gold\",\"Platinum\"],\"Stones\":[\"Diamond\"],\"StoneTypes\":[\"Precious\"],\"StoneColors\":[\"White\"],\"StoneShapes\":[\"Round\",\"Princess\"],\"StoneSizes\":[\"Medium\",\"Large\"]}",
                    StepId = 8
                },
                new Option
                {
                    Id = 42,
                    Name = "Birthday",
                    Content = "Colorful and joyful jewelry to celebrate you.",
                    FilterJson = "{\"Styles\":[\"Playful\",\"Modern\"],\"Metals\":[\"Silver\",\"Gold\"],\"Stones\":[\"Amethyst\",\"Topaz\",\"Citrine\"],\"StoneTypes\":[\"Semi-Precious\"],\"StoneColors\":[\"Purple\",\"Blue\",\"Yellow\"],\"StoneShapes\":[\"Oval\",\"Heart\"],\"StoneSizes\":[\"Medium\"]}",
                    StepId = 8
                },
                new Option
                {
                    Id = 43,
                    Name = "Corporate event",
                    Content = "Minimalistic and professional designs for a refined look.",
                    FilterJson = "{\"Styles\":[\"Minimalist\",\"Modern\",\"Elegant\"],\"Metals\":[\"Silver\",\"White Gold\"],\"StoneColors\":[\"White\",\"Black\",\"Blue\"],\"StoneSizes\":[\"Small\"],\"StoneTypes\":[\"Precious\",\"Semi-Precious\"]}",
                    StepId = 8
                },
                new Option
                {
                    Id = 44,
                    Name = "Romantic evening",
                    Content = "Charming pieces to make hearts flutter.",
                    FilterJson = "{\"Styles\":[\"Romantic\",\"Vintage\"],\"Metals\":[\"Rose Gold\",\"Gold\"],\"Stones\":[\"Ruby\",\"Sapphire\",\"Diamond\"],\"StoneColors\":[\"Red\",\"Pink\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Medium\"]}",
                    StepId = 8
                },
                new Option
                {
                    Id = 45,
                    Name = "Just want to treat myself",
                    Content = "Unique and expressive styles that reflect you.",
                    FilterJson = "{\"Styles\":[\"Boho\",\"Art Deco\",\"Modern\"],\"Metals\":[\"Silver\",\"Gold\",\"Mixed\"],\"Stones\":[\"Obsidian\",\"Labradorite\",\"Turquoise\"],\"StoneTypes\":[\"Organic\",\"Semi-Precious\"],\"StoneColors\":[\"Black\",\"Blue\",\"Green\"],\"StoneSizes\":[\"Medium\",\"Large\"]}",
                    StepId = 8
                }
            );

            // Scenario 2, Step 2, options (Mood selection)
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 46,
                    Name = "Bright and noticeable",
                    Content = "Bold and colorful pieces that draw attention.",
                    FilterJson = "{\"Styles\":[\"Bold\",\"Colorful\"],\"StoneColors\":[\"Red\",\"Yellow\",\"Turquoise\"],\"StoneSizes\":[\"Large\"],\"Metals\":[\"Gold\",\"Rose Gold\"]}",
                    StepId = 9
                },
                new Option
                {
                    Id = 47,
                    Name = "Subtle and elegant",
                    Content = "Simple, clean, and graceful designs.",
                    FilterJson = "{\"Styles\":[\"Minimalist\",\"Elegant\"],\"StoneColors\":[\"White\",\"Blue\",\"Black\"],\"StoneSizes\":[\"Small\"],\"Metals\":[\"Silver\",\"White Gold\"]}",
                    StepId = 9
                },
                new Option
                {
                    Id = 48,
                    Name = "Soft and romantic",
                    Content = "Delicate pieces with gentle tones and curves.",
                    FilterJson = "{\"Styles\":[\"Romantic\",\"Vintage\"],\"StoneColors\":[\"Pink\",\"Peach\",\"Purple\"],\"StoneShapes\":[\"Heart\",\"Oval\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Rose Gold\"]}",
                    StepId = 9
                },
                new Option
                {
                    Id = 49,
                    Name = "Luxurious and striking",
                    Content = "Statement jewelry for special moments.",
                    FilterJson = "{\"Styles\":[\"Luxury\",\"Statement\"],\"Stones\":[\"Diamond\",\"Ruby\",\"Emerald\"],\"StoneColors\":[\"White\",\"Red\",\"Green\"],\"StoneSizes\":[\"Large\"],\"Metals\":[\"Platinum\",\"Gold\"]}",
                    StepId = 9
                },
                new Option
                {
                    Id = 50,
                    Name = "Versatile for any situation",
                    Content = "Classic designs that suit any look.",
                    FilterJson = "{\"Styles\":[\"Classic\",\"Universal\"],\"StoneColors\":[\"Neutral\",\"White\",\"Blue\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Silver\",\"Gold\"]}",
                    StepId = 9
                }
            );

            // Scenario 2, Step 3, options (Clothing style)
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 51,
                    Name = "Evening dress",
                    Content = "Perfect for formal events and glamorous evenings.",
                    FilterJson = "{\"Styles\":[\"Elegant\",\"Luxury\"],\"Stones\":[\"Diamond\",\"Sapphire\"],\"StoneSizes\":[\"Medium\",\"Large\"],\"Metals\":[\"Gold\",\"Platinum\"]}",
                    StepId = 10
                },
                new Option
                {
                    Id = 52,
                    Name = "Business style",
                    Content = "Discreet and refined jewelry for a professional look.",
                    FilterJson = "{\"Styles\":[\"Minimalist\",\"Classic\"],\"StoneColors\":[\"White\",\"Blue\",\"Black\"],\"StoneSizes\":[\"Small\"],\"Metals\":[\"Silver\",\"White Gold\"]}",
                    StepId = 10
                },
                new Option
                {
                    Id = 53,
                    Name = "Casual everyday",
                    Content = "Comfortable and easy-to-match accessories.",
                    FilterJson = "{\"Styles\":[\"Casual\",\"Simple\"],\"StoneColors\":[\"Neutral\",\"Light Blue\"],\"StoneSizes\":[\"Small\",\"Medium\"],\"Metals\":[\"Silver\"]}",
                    StepId = 10
                },
                new Option
                {
                    Id = 54,
                    Name = "Smart casual",
                    Content = "Stylish and versatile pieces that elevate your outfit.",
                    FilterJson = "{\"Styles\":[\"Smart\",\"Modern\"],\"StoneColors\":[\"Green\",\"Beige\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Rose Gold\",\"Gold\"]}",
                    StepId = 10
                },
                new Option
                {
                    Id = 55,
                    Name = "Not sure / Doesn’t matter",
                    Content = "Let us surprise you with something universally flattering.",
                    FilterJson = "{\"Styles\":[\"Universal\",\"Classic\"],\"StoneSizes\":[\"Medium\"],\"Metals\":[\"Silver\",\"Gold\"]}",
                    StepId = 10
                }
            );

            // Scenario 2, Step 4, options (Preferred metal)
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 56,
                    Name = "Gold",
                    Content = "Warm and timeless, perfect for any skin tone.",
                    FilterJson = "{\"Metals\":[\"Gold\"]}",
                    StepId = 11
                },
                new Option
                {
                    Id = 57,
                    Name = "Silver",
                    Content = "Cool, elegant, and versatile for all occasions.",
                    FilterJson = "{\"Metals\":[\"Silver\"]}",
                    StepId = 11
                },
                new Option
                {
                    Id = 58,
                    Name = "Platinum",
                    Content = "Rare and durable, ideal for luxury lovers.",
                    FilterJson = "{\"Metals\":[\"Platinum\"]}",
                    StepId = 11
                },
                new Option
                {
                    Id = 59,
                    Name = "Dark metal / Oxidized",
                    Content = "Edgy and bold, great for making a statement.",
                    FilterJson = "{\"MetalColors\":[\"Dark\",\"Oxidized\"]}",
                    StepId = 11
                },
                new Option
                {
                    Id = 60,
                    Name = "No preference",
                    Content = "We’ll show you a mix of beautiful metal options.",
                    FilterJson = "{}",
                    StepId = 11
                }
            );

            // Scenario 2, Step 5, options (Type of jewelry)
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 61,
                    Name = "Ring",
                    Content = "Classic or statement pieces to enhance any look.",
                    FilterJson = "{\"Types\":[\"Ring\"]}",
                    StepId = 12
                },
                new Option
                {
                    Id = 62,
                    Name = "Necklace",
                    Content = "From delicate chains to bold pendants.",
                    FilterJson = "{\"Types\":[\"Necklace\"]}",
                    StepId = 12
                },
                new Option
                {
                    Id = 63,
                    Name = "Bracelet",
                    Content = "Perfect for layering or wearing solo.",
                    FilterJson = "{\"Types\":[\"Bracelet\"]}",
                    StepId = 12
                },
                new Option
                {
                    Id = 64,
                    Name = "Earrings",
                    Content = "Studs, hoops, or chandeliers to match any outfit.",
                    FilterJson = "{\"Types\":[\"Earrings\"]}",
                    StepId = 12
                },
                new Option
                {
                    Id = 65,
                    Name = "No preference",
                    Content = "We’ll show a mix of beautiful jewelry types.",
                    FilterJson = "{}",
                    StepId = 12
                }
            );


            // Scenario 3, steps
            modelBuilder.Entity<Step>().HasData(
                new Step
                {
                    Id = 13,
                    Name = "Jewelry Type",
                    QuestionText = "What type of jewelry would you like to design?",
                    NextStepId = 14,
                    ScenarioId = 3
                },
                new Step
                {
                    Id = 14,
                    Name = "Base Metal",
                    QuestionText = "What metal do you prefer for the base?",
                    NextStepId = 15,
                    ScenarioId = 3
                },
                new Step
                {
                    Id = 15,
                    Name = "Metal Color",
                    QuestionText = "What color or finish should the metal have?",
                    NextStepId = 16,
                    ScenarioId = 3
                },
                new Step
                {
                    Id = 16,
                    Name = "Add Stones",
                    QuestionText = "Would you like to add any stones?",
                    NextStepId = 17,
                    ScenarioId = 3
                },
                new Step
                {
                    Id = 17,
                    Name = "Stone Shape & Size",
                    QuestionText = "What shape and size should the stones be?",
                    NextStepId = 18,
                    ScenarioId = 3
                },
                new Step
                {
                    Id = 18,
                    Name = "Design Style",
                    QuestionText = "Do you have a preferred design or motif?",
                    ScenarioId = 3
                }
            );

            // Scenario 3, Step 1 (Jewelry Type) options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 66,
                    Name = "Ring",
                    Content = "A timeless piece for any occasion.",
                    FilterJson = "{\"ProductTypes\": [\"Ring\"]}",
                    StepId = 13
                },
                new Option
                {
                    Id = 67,
                    Name = "Necklace",
                    Content = "Elegant and expressive centerpiece.",
                    FilterJson = "{\"ProductTypes\": [\"Necklace\"]}",
                    StepId = 13
                },
                new Option
                {
                    Id = 68,
                    Name = "Earrings",
                    Content = "Perfect for adding a touch of sparkle.",
                    FilterJson = "{\"ProductTypes\": [\"Earrings\"]}",
                    StepId = 13
                },
                new Option
                {
                    Id = 69,
                    Name = "Bracelet",
                    Content = "Stylish and comfortable for everyday or formal wear.",
                    FilterJson = "{\"ProductTypes\": [\"Bracelet\"]}",
                    StepId = 13
                },
                new Option
                {
                    Id = 70,
                    Name = "Pendant",
                    Content = "Simple, symbolic, and beautiful.",
                    FilterJson = "{\"ProductTypes\": [\"Pendant\"]}",
                    StepId = 13
                }
            );

            // Scenario 3, Step 2 (Base Metal) options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 71,
                    Name = "Gold",
                    Content = "Warm and classic, ideal for timeless pieces.",
                    FilterJson = "{\"Metals\": [\"Gold\"]}",
                    StepId = 14
                },
                new Option
                {
                    Id = 72,
                    Name = "Silver",
                    Content = "Bright and elegant, perfect for versatile looks.",
                    FilterJson = "{\"Metals\": [\"Silver\"]}",
                    StepId = 14
                },
                new Option
                {
                    Id = 73,
                    Name = "Platinum",
                    Content = "Rare and durable, a symbol of lasting value.",
                    FilterJson = "{\"Metals\": [\"Platinum\"]}",
                    StepId = 14
                },
                new Option
                {
                    Id = 74,
                    Name = "Rose Gold",
                    Content = "Romantic and modern with a warm tone.",
                    FilterJson = "{\"MetalColors\": [\"Rose\"]}",
                    StepId = 14
                },
                new Option
                {
                    Id = 75,
                    Name = "Oxidized / Dark Metal",
                    Content = "Unique and bold, ideal for statement designs.",
                    FilterJson = "{\"MetalColors\": [\"Oxidized\", \"Dark\"]}",
                    StepId = 14
                }
            );

            // Scenario 3, Step 3 (Metal Color) options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 76,
                    Name = "Yellow Gold",
                    Content = "Classic warm tone, timeless and elegant.",
                    FilterJson = "{\"MetalColors\":[\"Yellow\"]}",
                    StepId = 15
                },
                new Option
                {
                    Id = 77,
                    Name = "White Gold",
                    Content = "Modern and sleek silver-like finish.",
                    FilterJson = "{\"MetalColors\":[\"White\"]}",
                    StepId = 15
                },
                new Option
                {
                    Id = 78,
                    Name = "Rose Gold",
                    Content = "Soft pinkish hue, romantic and trendy.",
                    FilterJson = "{\"MetalColors\":[\"Rose\"]}",
                    StepId = 15
                },
                new Option
                {
                    Id = 79,
                    Name = "Matte Finish",
                    Content = "Subtle, non-reflective surface for understated elegance.",
                    FilterJson = "{\"MetalFinishes\":[\"Matte\"]}",
                    StepId = 15
                },
                new Option
                {
                    Id = 80,
                    Name = "Polished Shine",
                    Content = "Bright and glossy finish for maximum brilliance.",
                    FilterJson = "{\"MetalFinishes\":[\"Polished\"]}",
                    StepId = 15
                },
                new Option
                {
                    Id = 81,
                    Name = "No preference",
                    Content = "Any metal color or finish is fine.",
                    FilterJson = "{}",
                    StepId = 15
                }
            );

            // Scenario 3, Step 4 (Add Stones) options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 82,
                    Name = "Yes, add diamonds",
                    Content = "Classic sparkle with timeless diamonds.",
                    FilterJson = "{\"Stones\": [\"Diamond\"]}",
                    StepId = 16
                },
                new Option
                {
                    Id = 83,
                    Name = "Yes, add colored gemstones",
                    Content = "Add vibrant color with sapphires, rubies, or emeralds.",
                    FilterJson = "{\"Stones\": [\"Sapphire\", \"Ruby\", \"Emerald\"]}",
                    StepId = 16
                },
                new Option
                {
                    Id = 84,
                    Name = "Yes, add pearls",
                    Content = "Soft elegance with natural pearls.",
                    FilterJson = "{\"Stones\": [\"Pearl\"]}",
                    StepId = 16
                },
                new Option
                {
                    Id = 85,
                    Name = "No stones",
                    Content = "Keep it simple and elegant without stones.",
                    FilterJson = "{}",
                    StepId = 16
                }
            );

            // Scenario 3, Step 5 (Stone Shape & Size) options
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 86,
                    Name = "Round, Medium",
                    Content = "Classic round shape, medium size for versatile use.",
                    FilterJson = "{\"StoneShapes\": [\"Round\"], \"StoneSizes\": [\"Regular\"]}",
                    StepId = 17
                },
                new Option
                {
                    Id = 87,
                    Name = "Oval, Large",
                    Content = "Elegant oval shape with large size for statement pieces.",
                    FilterJson = "{\"StoneShapes\": [\"Oval\"], \"StoneSizes\": [\"Large\"]}",
                    StepId = 17
                },
                new Option
                {
                    Id = 88,
                    Name = "Princess, Small",
                    Content = "Square princess cut, small size for subtle sparkle.",
                    FilterJson = "{\"StoneShapes\": [\"Princess\"], \"StoneSizes\": [\"Tiny\"]}",
                    StepId = 17
                },
                new Option
                {
                    Id = 89,
                    Name = "Mixed shapes and sizes",
                    Content = "A combination of different shapes and sizes for a unique design.",
                    FilterJson = "{\"StoneShapes\": [\"Round\", \"Oval\", \"Princess\"], \"StoneSizes\": [\"Tiny\", \"Regular\", \"Large\"]}",
                    StepId = 17
                },
                new Option
                {
                    Id = 90,
                    Name = "No preference",
                    Content = "I’m open to any shape or size.",
                    FilterJson = "{}",
                    StepId = 17
                }
            );

            // Scenario 3, Step 6 (Design Style) options — using Styles
            modelBuilder.Entity<Option>().HasData(
                new Option
                {
                    Id = 91,
                    Name = "Classic",
                    Content = "Timeless designs with elegant details.",
                    FilterJson = "{\"Styles\":[\"Classic\"]}",
                    StepId = 18
                },
                new Option
                {
                    Id = 92,
                    Name = "Modern",
                    Content = "Clean lines and minimalist shapes.",
                    FilterJson = "{\"Styles\":[\"Modern\"]}",
                    StepId = 18
                },
                new Option
                {
                    Id = 93,
                    Name = "Vintage",
                    Content = "Inspired by antique and retro aesthetics.",
                    FilterJson = "{\"Styles\":[\"Vintage\"]}",
                    StepId = 18
                },
                new Option
                {
                    Id = 94,
                    Name = "Floral",
                    Content = "Motifs featuring flowers and nature.",
                    FilterJson = "{\"Styles\":[\"Floral\"]}",
                    StepId = 18
                },
                new Option
                {
                    Id = 95,
                    Name = "Geometric",
                    Content = "Bold shapes and angular patterns.",
                    FilterJson = "{\"Styles\":[\"Geometric\"]}",
                    StepId = 18
                },
                new Option
                {
                    Id = 96,
                    Name = "No preference",
                    Content = "I’m open to any design style.",
                    FilterJson = "{}",
                    StepId = 18
                }
            );

            // Scenario 4, steps
            modelBuilder.Entity<Step>().HasData(
                new Step
                {
                    Id = 19,
                    Name = "Get photo",
                    QuestionText = "Please send a photo.",
                    ScenarioId = 4
                }
            );

            // Scenario 5, steps
            modelBuilder.Entity<Step>().HasData(
                new Step
                {
                    Id = 20,
                    Name = "Select category",
                    QuestionText = "Please select a category.",
                    NextStepId = 21,
                    ScenarioId = 5
                },
                new Step
                {
                    Id = 21,
                    Name = "Get photo",
                    QuestionText = "Please send a photo.",
                    ScenarioId = 5
                }
            );









        }
    }            
}
