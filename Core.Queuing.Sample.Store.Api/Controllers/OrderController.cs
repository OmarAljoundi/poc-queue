using Core.Queuing.Sample.Store.Api.Entity;
using Core.Queuing.Sample.Store.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core.Queuing.Sample.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInsert order)
        {
            try
            {
                return Ok(await _orderService.AddOrder(order));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}
