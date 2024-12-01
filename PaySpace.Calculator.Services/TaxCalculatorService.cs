using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Factory;
using PaySpace.Calculator.Services.Models;
using System.Text.Json;

namespace PaySpace.Calculator.Services
{
    internal sealed class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ICalculatorFactory _calculatorFactory;
        private readonly ILogger<TaxCalculatorService> _logger;

        public TaxCalculatorService(ICalculatorFactory calculatorFactory, ILogger<TaxCalculatorService> logger)
        {
            _calculatorFactory = calculatorFactory;
            _logger = logger;
        }

        public async Task<decimal> CalculateTaxAsync(TaxCalculator taxCalculator)
        {
            _logger.LogInformation($"TaxCalculatorService->CalculateTaxAsync start | TaxCalculator: {JsonSerializer.Serialize(taxCalculator)}");

            CalculateResult result = new CalculateResult();

            ITaxTypeCalculator calculator = _calculatorFactory.GetCalculator(taxCalculator.Calculator);
            result = await calculator.CalculateAsync(taxCalculator.Income);

            _logger.LogInformation("TaxCalculatorService->CalculateTaxAsync end");

            return result.Tax;
        }
    }
}