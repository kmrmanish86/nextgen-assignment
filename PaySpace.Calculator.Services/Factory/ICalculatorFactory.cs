using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculator.Services.Factory
{
    public interface ICalculatorFactory
    {
        public ITaxTypeCalculator GetCalculator(CalculatorType type);
    }
}
