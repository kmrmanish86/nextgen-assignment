using PaySpace.Calculator.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.API.Models
{
    public sealed class CalculatorSettingDto
    {
        public CalculatorType Calculator { get; set; }

        public RateType RateType { get; set; }

        public decimal Rate { get; set; }

        public decimal From { get; set; }

        public decimal? To { get; set; }
    }
}