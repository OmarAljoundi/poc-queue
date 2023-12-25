
using Microsoft.AspNetCore.Mvc;

namespace Core.Queuing.Sample.Crm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {


        public CustomerController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                return Ok("Success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /*[HttpPost]
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
        }*/


    }
}
