using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator(ICalculatorSettingsService calculatorSettingsService) : IProgressiveCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal tax = 0;

            var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.Progressive);

            if (settings.Count > 0)
            {
                foreach (var setting in settings)
                {
                    if (!setting.To.HasValue || income <= setting.To)
                    {
                        tax += (income * setting.Rate) / 100;
                        income = 0;
                    }
                    else if (income > setting.To)
                    {
                        decimal taxableIncom = setting.To.Value - setting.From;
                        tax += (taxableIncom * setting.Rate) / 100;
                        income = income - setting.To.Value;
                    }

                    if (income == 0)
                    {
                        break;
                    }
                }
            }

            return new CalculateResult { Calculator = CalculatorType.Progressive, Tax = tax };
        }
    }
}