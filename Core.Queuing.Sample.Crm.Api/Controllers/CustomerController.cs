using Core.Logging.OpenSearch.Abstraction;
using Core.Queuing.Sample.Crm.Api.Entity;
using Core.Queuing.Sample.Crm.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core.Queuing.Sample.Crm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly ILogging _log;


        public CustomerController(ICustomerService customerService,ILogging log)
        {
            _customerService = customerService;
            _log = log;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerInsert customer)
        {
            try
            {
                await _log.Log(new Logging.OpenSearch.Model.LogItem());
                return Ok(await _customerService.AddCustomer(customer));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            try
            {
                return Ok(await _customerService.UpdateCustomer(customer));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
