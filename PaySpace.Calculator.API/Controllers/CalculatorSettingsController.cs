using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;
using System.Text.Json;

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
            logger.LogInformation($"CalculatorSettingsController->CalculatorSettings Start | CalculatorType: {JsonSerializer.Serialize(calculatorType)}");

            try
            {
                var settings = await calculatorSettingsService.GetSettingsAsync(calculatorType);

                logger.LogInformation("CalculatorSettingsController->CalculatorSettings | settings fetched successfully");

                return this.Ok(mapper.Map<List<CalculatorSettingDto>>(settings));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}