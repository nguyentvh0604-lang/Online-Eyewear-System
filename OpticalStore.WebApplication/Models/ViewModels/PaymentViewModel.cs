using System.ComponentModel.DataAnnotations;

namespace OpticalStore.WebApplication.Models.ViewModels
{
    public class PaymentViewModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string CustomerName { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentMethod { get; set; } = "COD";

        public decimal TotalAmount { get; set; }
    }
}
