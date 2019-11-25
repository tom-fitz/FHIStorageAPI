﻿// <auto-generated />
using System;
using FHIStorage.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FHIStorage.API.Migrations
{
    [DbContext(typeof(HouseInfoContext))]
    [Migration("20190926200402_adding")]
    partial class adding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FHIStorage.API.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryGroupId");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.CategoryGroup", b =>
                {
                    b.Property<int>("CategoryGroupId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupType");

                    b.HasKey("CategoryGroupId");

                    b.ToTable("CategoryGroups");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.Furniture", b =>
                {
                    b.Property<int>("FurnitureId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<decimal?>("Cost")
                        .IsRequired();

                    b.Property<DateTime?>("DatePurchased");

                    b.Property<int?>("FurnitureImageId");

                    b.Property<int?>("Height");

                    b.Property<int>("HouseId");

                    b.Property<bool>("IsFurnitureSet");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PurchasedFrom");

                    b.Property<int>("Quantity");

                    b.Property<int>("Turns");

                    b.Property<string>("UID")
                        .IsRequired();

                    b.Property<int?>("Width");

                    b.HasKey("FurnitureId");

                    b.HasIndex("HouseId");

                    b.ToTable("Furniture");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.FurnitureImage", b =>
                {
                    b.Property<int>("FurnitureImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FurnitureId");

                    b.Property<string>("PictureInfo")
                        .IsRequired();

                    b.HasKey("FurnitureImageId");

                    b.HasIndex("FurnitureId");

                    b.ToTable("FurnitureImages");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.FurnitureSet", b =>
                {
                    b.Property<int>("FurnitureSetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FurnitureId");

                    b.Property<int>("HouseId");

                    b.Property<int>("MasterFurnitureId");

                    b.HasKey("FurnitureSetId");

                    b.ToTable("FurnitureSets");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.House", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("ContractDate");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime?>("DateSold");

                    b.Property<bool>("Sold");

                    b.Property<int>("Zipcode");

                    b.HasKey("HouseId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("FHIStorage.API.Entities.Furniture", b =>
                {
                    b.HasOne("FHIStorage.API.Entities.House", "House")
                        .WithMany()
                        .HasForeignKey("HouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FHIStorage.API.Entities.FurnitureImage", b =>
                {
                    b.HasOne("FHIStorage.API.Entities.Furniture")
                        .WithMany("FurnitureImages")
                        .HasForeignKey("FurnitureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
