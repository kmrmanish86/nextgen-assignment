using Microsoft.EntityFrameworkCore;

using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Factory;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services
{
    internal sealed class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public TaxCalculatorService(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public async Task<decimal> CalculateTaxAsync(TaxCalculator taxCalculator)
        {
            CalculateResult result = new CalculateResult();

            ITaxTypeCalculator calculator = _calculatorFactory.GetCalculator(taxCalculator.Calculator);
            result = await calculator.CalculateAsync(taxCalculator.Income);

            return result.Tax;
        }
    }
}