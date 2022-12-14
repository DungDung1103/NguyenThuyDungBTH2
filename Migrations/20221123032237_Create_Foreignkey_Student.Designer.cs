// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NguyenThuyDungBTH2.Data;

#nullable disable

namespace NguyenThuyDungBTH2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221123032237_Create_Foreignkey_Student")]
    partial class Create_Foreignkey_Student
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerAge")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeID")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeAge")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Faculty", b =>
                {
                    b.Property<string>("FacultyID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FacultyName")
                        .HasColumnType("TEXT");

                    b.HasKey("FacultyID");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Person", b =>
                {
                    b.Property<string>("PersonID")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonAge")
                        .HasColumnType("TEXT");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Student", b =>
                {
                    b.Property<string>("StudentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FacultyID")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentID");

                    b.HasIndex("FacultyID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("NguyenThuyDungBTH2.Models.Student", b =>
                {
                    b.HasOne("NguyenThuyDungBTH2.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyID");

                    b.Navigation("Faculty");
                });
#pragma warning restore 612, 618
        }
    }
}
