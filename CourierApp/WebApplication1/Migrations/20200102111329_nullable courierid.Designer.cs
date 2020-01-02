﻿// <auto-generated />
using System;
using CourierApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CourierApp.WebApp.Migrations
{
    [DbContext(typeof(CourierAppDbContext))]
    [Migration("20200102111329_nullable courierid")]
    partial class nullablecourierid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CourierApp.Data.Models.Courier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Couriers");
                });

            modelBuilder.Entity("CourierApp.Data.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("CourierId");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CourierId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("CourierApp.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Content")
                        .HasMaxLength(250);

                    b.Property<int>("CourierId");

                    b.Property<int>("Mark");

                    b.HasKey("Id");

                    b.HasIndex("CourierId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CourierApp.Data.Models.ReviewLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<int>("CourierId");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("ReviewLinks");
                });

            modelBuilder.Entity("CourierApp.Data.Models.Package", b =>
                {
                    b.HasOne("CourierApp.Data.Models.Courier", "Courier")
                        .WithMany("Packages")
                        .HasForeignKey("CourierId");
                });

            modelBuilder.Entity("CourierApp.Data.Models.Review", b =>
                {
                    b.HasOne("CourierApp.Data.Models.Courier", "Courier")
                        .WithMany("Reviews")
                        .HasForeignKey("CourierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
