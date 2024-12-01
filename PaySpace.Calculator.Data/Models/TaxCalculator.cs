using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models
{
    public sealed class TaxCalculator
    {
        public string? PostalCode { get; set; }

        public decimal Income { get; set; }

        public CalculatorType Calculator { get; set; }
    }
}