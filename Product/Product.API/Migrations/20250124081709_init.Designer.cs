﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.Api;

#nullable disable

namespace Product.Api.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20250124081709_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Clothing"
                        });
                });

            modelBuilder.Entity("Product.Model.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Name = "Laptop",
                            Price = 1000m
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 2,
                            Name = "T-Shirt",
                            Price = 20m
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 1,
                            Name = "Smartphone",
                            Price = 700m
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 1,
                            Name = "Headphones",
                            Price = 100m
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 1,
                            Name = "Camera",
                            Price = 500m
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 2,
                            Name = "Watch",
                            Price = 150m
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 2,
                            Name = "Shoes",
                            Price = 80m
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 2,
                            Name = "Jacket",
                            Price = 120m
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 1,
                            Name = "Tablet",
                            Price = 300m
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 1,
                            Name = "Mouse",
                            Price = 50m
                        });
                });

            modelBuilder.Entity("Product.Model.ProductDetail", b =>
                {
                    b.Property<int>("ProductDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductDetailId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ProductDetailId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductDetails");

                    b.HasData(
                        new
                        {
                            ProductDetailId = 1,
                            Description = "High-performance laptop",
                            ProductId = 1
                        },
                        new
                        {
                            ProductDetailId = 2,
                            Description = "Comfortable cotton t-shirt",
                            ProductId = 2
                        },
                        new
                        {
                            ProductDetailId = 3,
                            Description = "Latest smartphone with advanced features",
                            ProductId = 3
                        },
                        new
                        {
                            ProductDetailId = 4,
                            Description = "Noise-canceling headphones",
                            ProductId = 4
                        },
                        new
                        {
                            ProductDetailId = 5,
                            Description = "High-resolution digital camera",
                            ProductId = 5
                        },
                        new
                        {
                            ProductDetailId = 6,
                            Description = "Stylish analog watch",
                            ProductId = 6
                        },
                        new
                        {
                            ProductDetailId = 7,
                            Description = "Durable running shoes",
                            ProductId = 7
                        },
                        new
                        {
                            ProductDetailId = 8,
                            Description = "Warm winter jacket",
                            ProductId = 8
                        },
                        new
                        {
                            ProductDetailId = 9,
                            Description = "Compact tablet for on-the-go use",
                            ProductId = 9
                        },
                        new
                        {
                            ProductDetailId = 10,
                            Description = "Ergonomic wireless mouse",
                            ProductId = 10
                        });
                });

            modelBuilder.Entity("Product.Model.Product", b =>
                {
                    b.HasOne("Product.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Product.Model.ProductDetail", b =>
                {
                    b.HasOne("Product.Model.Product", "Product")
                        .WithOne("ProductDetail")
                        .HasForeignKey("Product.Model.ProductDetail", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Product.Model.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Product.Model.Product", b =>
                {
                    b.Navigation("ProductDetail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
