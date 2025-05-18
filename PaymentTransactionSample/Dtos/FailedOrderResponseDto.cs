namespace PaymentTransactionSample.Dtos
{
    public class FailedOrderResponseDto : OrderResponseDto
    {
        public required string ResultMessage { get; set; }

    }
}
