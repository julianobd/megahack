﻿// <auto-generated />
using System;
using MegaHack5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MegaHack5.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MegaHack5.Models.BusinessOccupation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusinessOccupation");
                });

            modelBuilder.Entity("MegaHack5.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FantasyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("MegaHack5.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("MegaHack5.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlanningId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlanningId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("MegaHack5.Models.InternalInvestment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("InvestmentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PlanningId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PlanningId");

                    b.ToTable("InternalInvestment");
                });

            modelBuilder.Entity("MegaHack5.Models.Maturity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Maturity");
                });

            modelBuilder.Entity("MegaHack5.Models.Planning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BusinessOccupationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CashOnHand")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InvestmentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("MaturityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessOccupationId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("MaturityId");

                    b.HasIndex("StatusId");

                    b.ToTable("Planning");
                });

            modelBuilder.Entity("MegaHack5.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("MegaHack5.Models.Department", b =>
                {
                    b.HasOne("MegaHack5.Models.Company", null)
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("MegaHack5.Models.File", b =>
                {
                    b.HasOne("MegaHack5.Models.Planning", null)
                        .WithMany("Files")
                        .HasForeignKey("PlanningId");
                });

            modelBuilder.Entity("MegaHack5.Models.InternalInvestment", b =>
                {
                    b.HasOne("MegaHack5.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("MegaHack5.Models.Planning", null)
                        .WithMany("InternalInvestments")
                        .HasForeignKey("PlanningId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MegaHack5.Models.Planning", b =>
                {
                    b.HasOne("MegaHack5.Models.BusinessOccupation", "BusinessOccupation")
                        .WithMany()
                        .HasForeignKey("BusinessOccupationId");

                    b.HasOne("MegaHack5.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("MegaHack5.Models.Maturity", "Maturity")
                        .WithMany()
                        .HasForeignKey("MaturityId");

                    b.HasOne("MegaHack5.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("BusinessOccupation");

                    b.Navigation("Company");

                    b.Navigation("Maturity");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MegaHack5.Models.Company", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("MegaHack5.Models.Planning", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("InternalInvestments");
                });
#pragma warning restore 612, 618
        }
    }
}
