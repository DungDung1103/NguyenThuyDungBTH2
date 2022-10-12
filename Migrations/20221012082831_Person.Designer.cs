﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NguyenThuyDungBTH2.Data;

#nullable disable

namespace NguyenThuyDungBTH2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221012082831_Person")]
    partial class Person
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeID")
                        .HasColumnType("TEXT");

                    b.Property<int>("EmployeeAge")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Person", b =>
                {
                    b.Property<string>("PersonID")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonAge")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PersonName")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Student", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentName")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentID");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}