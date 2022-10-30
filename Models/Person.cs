using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace NguyenThuyDungBTH2.Models
{
    public class Person
    {
        [Key]
        [Required(ErrorMessage="Mã person không được để trống")]
        [MaxLength(5)]
        public string? PersonID { get; set; }

        [Required(ErrorMessage="Tên person không được để trống")]
        public string? PersonName { get; set; }
        public int PersonAge { get; set; }
    }
}