﻿// <auto-generated />
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210527103839_AddDepartmentAndDivisionRel")]
    partial class AddDepartmentAndDivisionRel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Models.DepartmentDivision", b =>
                {
                    b.Property<int>("Division_Id")
                        .HasColumnType("int");

                    b.Property<int>("Department_Id")
                        .HasColumnType("int");

                    b.HasKey("Division_Id", "Department_Id");

                    b.HasIndex("Department_Id");

                    b.ToTable("DepartmentDivisionsModel");
                });

            modelBuilder.Entity("Models.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("Models.DivisionEmployee", b =>
                {
                    b.Property<int>("Employee_Id")
                        .HasColumnType("int");

                    b.Property<int>("Division_Id")
                        .HasColumnType("int");

                    b.HasKey("Employee_Id", "Division_Id");

                    b.HasIndex("Division_Id");

                    b.ToTable("DivisionEmployeesModel");
                });

            modelBuilder.Entity("Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Models.EmployeePosition", b =>
                {
                    b.Property<int>("Position_Id")
                        .HasColumnType("int");

                    b.Property<int>("Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Position_Id", "Employee_Id");

                    b.HasIndex("Employee_Id");

                    b.ToTable("EmployeePositions");
                });

            modelBuilder.Entity("Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Models.DepartmentDivision", b =>
                {
                    b.HasOne("Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("Division_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Models.DivisionEmployee", b =>
                {
                    b.HasOne("Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("Division_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Models.EmployeePosition", b =>
                {
                    b.HasOne("Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("Position_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });
#pragma warning restore 612, 618
        }
    }
}
