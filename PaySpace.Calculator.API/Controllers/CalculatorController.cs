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
    public sealed class CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        ICalculatorSettingsService calculatorSettingsService,
        ITaxCalculatorService taxCalculatorService,
        IMapper mapper)
        : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {
                var result = await taxCalculatorService.CalculateTaxAsync(new TaxCalculator {
                    Income=request.Income,
                    PostalCode=request.PostalCode,
                    Calculator= (CalculatorType)Enum.Parse(typeof(CalculatorType), request.PostalCode)
                });

                await historyService.AddAsync(new CalculatorHistory
                {
                    Tax = result,
                    Calculator = (CalculatorType)Enum.Parse(typeof(CalculatorType), request.PostalCode),
                    PostalCode = request.PostalCode,
                    Income = request.Income
                });

                return this.Ok(mapper.Map<CalculateResultDto>(new { Calculator = request.PostalCode, Tax = result }));
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            var history = await historyService.GetHistoryAsync();

            return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));
        }
    }
}