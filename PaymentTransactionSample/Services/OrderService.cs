using log4net;
using PaymentTransactionSample.Data;
using PaymentTransactionSample.Dtos;
using PaymentTransactionSample.Exceptions;
using PaymentTransactionSample.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace PaymentTransactionSample.Services
{
    public class OrderService : IOrderService
    {
        public OrderResponseDto CreateOrder(OrderDto order)
        {
            //--- validation start ----
            if (!IsValidPartner(order.PartnerKey, order.PartnerPassword))
            {
                return new FailedOrderResponseDto
                {
                    Result = 0,
                    ResultMessage = "Access Denied!",
                };
            }

            if (!IsAmoutMatch(order))
            {
                return new FailedOrderResponseDto
                {
                    Result = 0,
                    ResultMessage = "Invalid Total Amount.",
                };
            }

            if (!IsValidTimeStamp(order.TimeStamp))
            {
                return new FailedOrderResponseDto
                {
                    Result = 0,
                    ResultMessage = "Expired.",
                };
            }

            //if (!IsValidSignature(order))
            //{
            //    return new FailedOrderResponseDto
            //    {
            //        Result = 0,
            //        ResultMessage = "Invalid Signature.",
            //    };
            //}


            //--- validation end ----

            long total_discount = GetDiscount(order.TotalAmount);

            return new SuccessOrderResponseDto
            {
                Result = 1,
                TotalAmount = order.TotalAmount,
                TotalDiscount = total_discount,
                FinalAmount = order.TotalAmount - total_discount,
            };
        }

        private PartnerDto? GetPartner(string partnerkey, string partnerpassword)
        {
            return TestData.Partners.FirstOrDefault(
               x => partnerkey.Equals(x.PartnerKey) && partnerpassword.ToBase64().Equals(x.PartnerPassword));
        }

        private bool IsValidPartner(string partnerkey, string partnerpassword)
        {
            if (GetPartner(partnerkey, partnerpassword) == null)
            {
                return false;
            }
            return true;
        }

        public string GenerateSignature(SignatureDto s_input)
        {
            string timestamp = s_input.TimeStamp.ToString("yyyyMMddHHmmss");
            string partner_key = s_input.PartnerKey;
            string total_amout = s_input.TotalAmount.ToString();
            string partner_password_encodeded = s_input.PartnerPassword.ToBase64();

            string signature = timestamp + partner_key + total_amout + partner_password_encodeded;

            return signature.ToSha256().ToBase64();
        }

        private bool IsValidSignature(OrderDto order)
        {
            SignatureDto signatureDto = new SignatureDto
            {
                PartnerKey = order.PartnerKey,
                PartnerPassword = order.PartnerPassword,
                TimeStamp = order.TimeStamp,
                TotalAmount = order.TotalAmount,
            };
            if (GenerateSignature(signatureDto).Equals(order.Sig))
            {
                return true;
            }
            return false;
        }

        private long CalculateAllItemsAmount(List<OrderItemDto> orderItems)
        {
            long total = 0;
            foreach (OrderItemDto orderItem in orderItems)
            {
                total += (orderItem.Qty * orderItem.UnitPrice);
            }
            Console.WriteLine($"Sum Item {total.ToString()}");
            return total;
        }

        private bool IsAmoutMatch(OrderDto order)
        {
            // skip validation when no order item is provided.
            if (order.Items == null || order.Items.Count() == 0)
            {
                return true;
            }

            Console.WriteLine($"Original amount {order.TotalAmount.ToString()}");

            if (order.TotalAmount.Equals(CalculateAllItemsAmount(order.Items)))
            {
                return true;
            }
            return false;
        }

        private bool IsValidTimeStamp(DateTime timestamp)
        {
            Console.WriteLine(DateTime.Now.AddMinutes(-5));
            Console.WriteLine(DateTime.Now.AddMinutes(5));

            if (timestamp < DateTime.Now.AddMinutes(-5) || timestamp > DateTime.Now.AddMinutes(5))
            {
                return false;
            }
            return true;

        }

        private long GetDiscount(long total_amount)
        {
            long discount_percentage = 0;
            long total_discount = 0;

            // no discount
            if(total_amount < 20000)
            {
                return 0;
            }

            //Base Discount
            if (total_amount >= 20000 && total_amount <= 50000)
            {
                discount_percentage += 5;
            }
            else if (total_amount > 50000 && total_amount <= 80000)
            {
                discount_percentage += 7;
            }
            else if (total_amount > 80000 && total_amount <= 120000)
            {
                discount_percentage += 10;
            }
            else if (total_amount > 120000)
            {
                discount_percentage += 15;
            }

            //Conditional Discounts
            if(total_amount > 50000 && total_amount.IsPrime())
            {
                discount_percentage += 8;
            }

            Console.WriteLine($"IsPrime() {total_amount.IsPrime()}");


            if (total_amount > 90000 && total_amount.IsThirdLastDigitFive())
            {
                discount_percentage += 10;
            }

            Console.WriteLine($"total_amount.IsThirdLastDigitFive() {total_amount.IsThirdLastDigitFive()}");



            // enforce maximum 20% discount
            if (discount_percentage > 20)
            {
                discount_percentage = 20;
            }

            Console.WriteLine($"Total Discount {discount_percentage}");

            total_discount = total_amount * discount_percentage / 100;

            return total_discount;

        }

    }
}
