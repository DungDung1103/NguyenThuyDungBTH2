using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace NguyenThuyDungBTH2.Models
{
    public class Student{
        // khai báo thuộc tính
        [Key]
        [Required(ErrorMessage="Mã sinh viên không được để trống")]
        public string? StudentID { get; set; }

        [Required(ErrorMessage="Tên sinh viên không được để trống")]
        public string? StudentName { get; set; }
        public string? FacultyID { get; set; }
        [ForeignKey("FacultyID")]
        public Faculty? Faculty { get;set; }
        
    }
}
