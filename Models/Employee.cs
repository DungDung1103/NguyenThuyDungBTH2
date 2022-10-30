using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace NguyenThuyDungBTH2.Models
{
    public class Employee
    {
        [Key]
        [Required(ErrorMessage="Mã nhân viên không được để trống")]
        [MaxLength(5)]
        public string? EmployeeID { get; set; }
        [Required(ErrorMessage="Tên nhân viên không được để trống")]
        public string? EmployeeName { get; set; }
        public int EmployeeAge { get; set; }
    }
}