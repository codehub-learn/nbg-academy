﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TinyCrm.Core.Data;

namespace ConsoleApp2.Migrations
{
    [DbContext(typeof(CrmDbContext))]
    [Migration("20210128073124_New-Migration2")]
    partial class NewMigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TinyCrm.Core.Model.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalGross")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VatNumber")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CustomerId");

                    b.HasIndex("VatNumber")
                        .IsUnique()
                        .HasFilter("[VatNumber] IS NOT NULL");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TinyCrm.Core.Model.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("TinyCrm.Core.Model.Order", b =>
                {
                    b.HasOne("TinyCrm.Core.Model.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TinyCrm.Core.Model.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
