﻿// <auto-generated />
using System;
using LeasingCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LeasingCore.Migrations
{
    [DbContext(typeof(LeasingContext))]
    [Migration("20181203230500_CreateLeasingDB")]
    partial class CreateLeasingDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LeasingCore.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LeasingCore.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyHeadquarters")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CompanyNIP")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CompanyId");

                    b.ToTable("Companys");
                });

            modelBuilder.Entity("LeasingCore.Models.Leasing", b =>
                {
                    b.Property<int>("LeasingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LeasingEnd");

                    b.Property<bool>("LeasingExtend");

                    b.Property<DateTime>("LeasingStart");

                    b.Property<int>("ProductId");

                    b.Property<int>("UserId");

                    b.HasKey("LeasingId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Leasings");
                });

            modelBuilder.Entity("LeasingCore.Models.LeasingDetail", b =>
                {
                    b.Property<int>("LeasingDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeasingDetailAmount");

                    b.Property<int>("LeasingId");

                    b.Property<int?>("ProductId");

                    b.HasKey("LeasingDetailId");

                    b.HasIndex("LeasingId");

                    b.HasIndex("ProductId");

                    b.ToTable("LeasingDetails");
                });

            modelBuilder.Entity("LeasingCore.Models.LeasingDetailParam", b =>
                {
                    b.Property<int>("LeasingDetailParamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeasingDetailId");

                    b.Property<int>("ParamAssortmentId");

                    b.Property<int?>("ParamId");

                    b.HasKey("LeasingDetailParamId");

                    b.HasIndex("LeasingDetailId");

                    b.HasIndex("ParamAssortmentId");

                    b.HasIndex("ParamId");

                    b.ToTable("LeasingDetailParams");
                });

            modelBuilder.Entity("LeasingCore.Models.Param", b =>
                {
                    b.Property<int>("ParamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParamName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("ParamId");

                    b.ToTable("Params");
                });

            modelBuilder.Entity("LeasingCore.Models.ParamAssortment", b =>
                {
                    b.Property<int>("ParamAssortmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParamAssortmentBrand")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("ParamAssortmentName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("ParamId");

                    b.HasKey("ParamAssortmentId");

                    b.HasIndex("ParamId");

                    b.ToTable("ParamAssortments");
                });

            modelBuilder.Entity("LeasingCore.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<int>("ProductAvailability");

                    b.Property<string>("ProductCode")
                        .IsRequired();

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<decimal>("ProductPrice");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LeasingCore.Models.ProductParam", b =>
                {
                    b.Property<int>("ProductParamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ParamId");

                    b.Property<int>("ProductId");

                    b.HasKey("ProductParamId");

                    b.HasIndex("ParamId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductParams");
                });

            modelBuilder.Entity("LeasingCore.Models.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeasingId");

                    b.Property<string>("ReportDescription")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("StatusId");

                    b.HasKey("ReportId");

                    b.HasIndex("LeasingId");

                    b.HasIndex("StatusId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("LeasingCore.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LeasingCore.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("LeasingCore.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyId");

                    b.Property<int>("RoleId");

                    b.Property<string>("UserEmail");

                    b.Property<string>("UserFirstName")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("UserPassword")
                        .HasMaxLength(20);

                    b.Property<string>("UserSurname")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LeasingCore.Models.Leasing", b =>
                {
                    b.HasOne("LeasingCore.Models.Product", "Product")
                        .WithMany("Leasings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.User", "User")
                        .WithMany("Leasings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeasingCore.Models.LeasingDetail", b =>
                {
                    b.HasOne("LeasingCore.Models.Leasing", "Leasing")
                        .WithMany("LeasingDetails")
                        .HasForeignKey("LeasingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.Product", "Product")
                        .WithMany("LeasingDetails")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("LeasingCore.Models.LeasingDetailParam", b =>
                {
                    b.HasOne("LeasingCore.Models.LeasingDetail", "LeasingDetail")
                        .WithMany("LeasingDetailParams")
                        .HasForeignKey("LeasingDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.ParamAssortment", "ParamAssortment")
                        .WithMany()
                        .HasForeignKey("ParamAssortmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.Param", "Param")
                        .WithMany()
                        .HasForeignKey("ParamId");
                });

            modelBuilder.Entity("LeasingCore.Models.ParamAssortment", b =>
                {
                    b.HasOne("LeasingCore.Models.Param", "Param")
                        .WithMany("ParamAssortments")
                        .HasForeignKey("ParamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeasingCore.Models.Product", b =>
                {
                    b.HasOne("LeasingCore.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeasingCore.Models.ProductParam", b =>
                {
                    b.HasOne("LeasingCore.Models.Param", "Param")
                        .WithMany("ProductParams")
                        .HasForeignKey("ParamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.Product", "Product")
                        .WithMany("ProductParams")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeasingCore.Models.Report", b =>
                {
                    b.HasOne("LeasingCore.Models.Leasing", "Leasing")
                        .WithMany("Reports")
                        .HasForeignKey("LeasingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.Status", "Status")
                        .WithMany("Reports")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LeasingCore.Models.User", b =>
                {
                    b.HasOne("LeasingCore.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LeasingCore.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}