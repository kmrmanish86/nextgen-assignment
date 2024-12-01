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
    public sealed class CalculatorSettingsController(
        ILogger<CalculatorSettingsController> logger,
        ICalculatorSettingsService calculatorSettingsService,
        IMapper mapper)
        : ControllerBase
    {        
        [HttpGet("calculatorsettings")]
        public async Task<ActionResult<List<CalculatorSetting>>> CalculatorSettings(CalculatorType calculatorType)
        {
            try
            {
                var postalcode = await calculatorSettingsService.GetSettingsAsync(calculatorType);

                return this.Ok(mapper.Map<List<CalculatorSettingDto>>(postalcode));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}