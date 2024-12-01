using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator(ICalculatorSettingsService calculatorSettingsService) : IFlatRateCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal tax = 0;

            var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatRate);
            if (settings.Count > 0)
            {
                tax = (income * (decimal)settings.First().Rate) / 100;
            }

            return new CalculateResult { Calculator = CalculatorType.FlatRate, Tax = tax };
        }
    }
}