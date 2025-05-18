using PaymentTransactionSample.Dtos;

namespace PaymentTransactionSample.Services
{
    public interface IOrderService
    {
        OrderResponseDto CreateOrder(OrderDto order);
        string GenerateSignature(SignatureDto s_input);

    }
}
