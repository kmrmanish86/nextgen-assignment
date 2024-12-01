using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class PostalCodeController(
        ILogger<PostalCodeController> logger,
        IPostalCodeService postalCodeService,
        IMapper mapper)
        : ControllerBase
    {        
        [HttpGet("postalcode")]
        public async Task<ActionResult<List<PostalCode>>> PostalCode()
        {
            logger.LogInformation("PostalCodeController->PostalCode start");

            try
            {
                var postalcode = await postalCodeService.GetPostalCodesAsync();

                logger.LogInformation("PostalCodeController->PostalCode | Postal Code fetched successfully");

                return this.Ok(mapper.Map<List<PostalCodeDto>>(postalcode));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}