using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentTransactionSample.Dtos;
using PaymentTransactionSample.Exceptions;
using PaymentTransactionSample.Extensions;
using PaymentTransactionSample.Services;
using Serilog;
using System.Text;
using System.Text.Json;

namespace PaymentTransactionSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Create")]
        public IActionResult CreateOrder([FromBody] OrderDto order)
        {

            LogHttpMessage(order);

            OrderResponseDto result = _orderService.CreateOrder(order);

            if(result.Result == 0)
            {
                LogHttpMessage((FailedOrderResponseDto)result);
                return BadRequest(result);
            }

            LogHttpMessage((SuccessOrderResponseDto)result);

            return Ok(result);
        }

        [HttpPost("Test/GenerateSignature")]
        public IActionResult GenerateSignature([FromBody] SignatureDto signatureDto)
        {

            string result = _orderService.GenerateSignature(signatureDto);

            return Ok(result);
        }

        private void LogHttpMessage( object obj)
        {
            //Log.ForContext("Tag", "Http").Information(JsonSerializer.Serialize(obj.GetType()));
            //Log.ForContext("Tag", "Http").Information(JsonSerializer.Serialize(typeof(OrderDto)));
            if (obj.GetType() == typeof(OrderDto))
            {
                // to prevent overriding same value
                var json = JsonSerializer.Serialize(obj);
                OrderDto orderDto = JsonSerializer.Deserialize<OrderDto>(json);
                orderDto.PartnerPassword = orderDto.PartnerPassword.ToBase64();

                Log.ForContext("Tag", "Http").Information(JsonSerializer.Serialize(orderDto));

            }
            else
            {
                Log.ForContext("Tag", "Http").Information(JsonSerializer.Serialize(obj));

            }
        }
    }
}
