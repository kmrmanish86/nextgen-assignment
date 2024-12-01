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
    public sealed class CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        ITaxCalculatorService taxCalculatorService,
        IMapper mapper)
        : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            logger.LogInformation($"CalculatorController->Calculate Start | CalculateRequest: {JsonSerializer.Serialize(request)}");

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

                logger.LogInformation($"CalculatorController->Calculate End");

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
            logger.LogInformation("CalculatorController->History Start");

            try
            {
                var history = await historyService.GetHistoryAsync();

                logger.LogInformation("CalculatorController->History History fetched successfully");

                return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}