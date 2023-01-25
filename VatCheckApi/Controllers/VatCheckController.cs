using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VatCheck.API.Helpers;
using VatCheck.Service.Abstract;

namespace VatCheck.API.Controllers
{

    [ApiController]
    public class VatCheckController : ControllerBase
    {
        private readonly IVatCheckService vatCheckService;

        public VatCheckController(IVatCheckService vatCheckService)
        {
            this.vatCheckService = vatCheckService;
        }

        [HttpGet]
        [Route("api/vatcheck/{vatNumber}")]
        public async Task<IActionResult> GetVat(string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(vatNumber))
            {
                return BadRequest();
            }
            //Insert Validation srvice and perform on param;

            try
            {

                return Ok(await vatCheckService.CheckVat(vatNumber));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
