namespace PaymentTransactionSample.Dtos
{
    public class SuccessOrderResponseDto : OrderResponseDto
    {
        public long TotalAmount { get; set; }
        public long TotalDiscount { get; set; }
        public long FinalAmount { get; set; }
    }
}
