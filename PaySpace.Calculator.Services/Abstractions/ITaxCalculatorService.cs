using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculatorService
    {
        Task<decimal> CalculateTaxAsync(TaxCalculator taxCalculator);
    }
}