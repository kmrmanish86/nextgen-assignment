using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator(ICalculatorSettingsService calculatorSettingsService) : IFlatValueCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal tax = 0;

            var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatValue);

            if (settings.Count > 0)
            {
                var amountSettings = settings.Where(x => x.RateType == RateType.Amount).FirstOrDefault();
                var percentageSettings = settings.Where(x => x.RateType == RateType.Percentage).FirstOrDefault();

                if (amountSettings != null && percentageSettings != null)
                {
                    if (income <= percentageSettings.To)
                    {
                        tax = (income * percentageSettings.Rate) / 100;
                    }
                    else
                    {
                        tax = amountSettings.Rate;
                    }
                }
                else
                {
                    //settings are missing
                    throw new Exception("Settings for Flat Rate is missing");
                }
            }

            return new CalculateResult { Calculator = CalculatorType.FlatValue, Tax = tax };
        }
    }
}