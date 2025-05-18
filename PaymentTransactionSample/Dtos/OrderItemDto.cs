using System.ComponentModel.DataAnnotations;

namespace PaymentTransactionSample.Dtos
{
    public class OrderItemDto
    {
        [Display(Name = "Reference ID")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must not exceed 50 characters.")]
        public required string PartnerItemRef { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} must not exceed 100 characters.")]
        public required string Name { get; set; }


        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(0, 5, ErrorMessage = "Quantity must not exceed 5")]
        public int Qty { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "{0} is required.")]
        [Range(0, long.MaxValue, ErrorMessage = "Value must be a whole number in cents. Example: 1000 = MYR 10.00")]
        public long UnitPrice { get; set; }
    }
}
