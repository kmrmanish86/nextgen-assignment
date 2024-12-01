using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculatorViewModel
    {
        
        public SelectList PostalCodes { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Income is required.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Income must be greater than 0")]
        public decimal Income { get; set; }
    }
}