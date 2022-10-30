using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace NguyenThuyDungBTH2.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage="Mã khách hàng không được để trống")]
        [MaxLength(5)]
        public string? CustomerID { get; set; }
         [Required(ErrorMessage="Tên khách hàng không được để trống")]
        public string? CustomerName { get; set; }
        public int CustomerAge { get; set; }
    }
}