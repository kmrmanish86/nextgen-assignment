using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxTypeCalculator
    {
        Task<CalculateResult> CalculateAsync(decimal income);
    }
}