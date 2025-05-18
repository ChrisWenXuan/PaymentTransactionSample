using System.ComponentModel.DataAnnotations;

namespace PaymentTransactionSample.Dtos
{
    public class SignatureDto
    {
        [Display(Name = "Partner Key")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must not exceed 50 characters.")]
        public required string PartnerKey { get; set; }


        [Display(Name = "Partner Password")]
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must not exceed 50 characters.")]
        public required string PartnerPassword { get; set; }

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "{0} is required.")]
        public long TotalAmount { get; set; }


        [Display(Name = "Time Stamp")]
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime TimeStamp { get; set; }
    }
}
