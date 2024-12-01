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

    internal class CalculatorFactory : ICalculatorFactory
    {
        private readonly ICalculatorSettingsService _calculatorSettingsService;

        public CalculatorFactory(ICalculatorSettingsService calculatorSettingsService)
        {
            _calculatorSettingsService = calculatorSettingsService;
        }

        public ITaxTypeCalculator GetCalculator(CalculatorType type)
        {
            ITaxTypeCalculator taxTypeCalculator = null;

            switch (type)
            {
                case CalculatorType.Progressive:
                    taxTypeCalculator = new ProgressiveCalculator(_calculatorSettingsService);
                    break;
                case CalculatorType.FlatValue:
                    taxTypeCalculator = new FlatValueCalculator(_calculatorSettingsService);
                    break;
                case CalculatorType.FlatRate:
                    taxTypeCalculator = new FlatRateCalculator(_calculatorSettingsService);
                    break;
            }

            return taxTypeCalculator;
        }
    }
}
