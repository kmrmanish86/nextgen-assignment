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
            try
            {
                var postalcode = await postalCodeService.GetPostalCodesAsync();

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