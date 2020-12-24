﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartOrder.API.Model;

namespace SmartOrder.API.Migrations
{
    [DbContext(typeof(DB_A25A8E_orderspapiContext))]
    [Migration("20201210170712_temp")]
    partial class temp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartOrder.API.Model.Addresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("city")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<double>("Latitude")
                        .HasColumnName("latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longtitude")
                        .HasColumnName("longtitude")
                        .HasColumnType("float");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnName("number")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int?>("StoreId")
                        .HasColumnName("store_id")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnName("street")
                        .HasColumnType("nvarchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("Tk")
                        .IsRequired()
                        .HasColumnName("TK")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Categories", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("ParentId")
                        .HasColumnName("parent_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SmartOrder.API.Model.MenuItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnName("category")
                        .HasColumnType("nvarchar(80)")
                        .HasDefaultValueSql("('')")
                        .HasMaxLength(80);

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("StoreId")
                        .HasColumnName("store_id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("SmartOrder.API.Model.OrderItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MenuitemId")
                        .HasColumnName("menuitem_id")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnName("order_id")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuitemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date_created")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int?>("EstimatedMinutes")
                        .HasColumnName("estimated_minutes")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("status")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("StoreId")
                        .HasColumnName("store_id")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnName("total_price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SmartOrder.API.Model.StoreToCategories", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnName("category_id")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnName("store_id")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreToCategories");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Stores", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnName("address_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Users", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnName("phone")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("surname")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Addresses", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Stores", "Store")
                        .WithMany("Addresses")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Addresses__store__4CA06362");

                    b.HasOne("SmartOrder.API.Model.Users", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Addresses__user___4D94879B");
                });

            modelBuilder.Entity("SmartOrder.API.Model.Categories", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Categories", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK__Categorie__paren__4316F928");
                });

            modelBuilder.Entity("SmartOrder.API.Model.MenuItems", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Stores", "Store")
                        .WithMany("MenuItems")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__MenuItems__store__440B1D61")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartOrder.API.Model.OrderItems", b =>
                {
                    b.HasOne("SmartOrder.API.Model.MenuItems", "Menuitem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuitemId")
                        .HasConstraintName("FK__OrderItem__menui__5FB337D6")
                        .IsRequired();

                    b.HasOne("SmartOrder.API.Model.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderItem__order__5EBF139D")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartOrder.API.Model.Orders", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Stores", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Orders__store_id__534D60F1")
                        .IsRequired();

                    b.HasOne("SmartOrder.API.Model.Users", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Orders__user_id__5441852A")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartOrder.API.Model.StoreToCategories", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Categories", "Category")
                        .WithMany("StoreToCategories")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__StoreToCa__categ__44FF419A")
                        .IsRequired();

                    b.HasOne("SmartOrder.API.Model.Stores", "Store")
                        .WithMany("StoreToCategories")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__StoreToCa__store__45F365D3")
                        .IsRequired();
                });

            modelBuilder.Entity("SmartOrder.API.Model.Stores", b =>
                {
                    b.HasOne("SmartOrder.API.Model.Addresses", "Address")
                        .WithMany("Stores")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK__Stores__address___6EF57B66");
                });
#pragma warning restore 612, 618
        }
    }
}
