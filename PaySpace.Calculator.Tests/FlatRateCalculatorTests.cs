using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Moq;

using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Factory;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests
    {
        private ServiceProvider _serviceProvider;
        private ICalculatorSettingsService _flatRateCalculator;

        [SetUp]
        public void Setup()
        {
            // Step 1: Create a ServiceCollection
            var services = new ServiceCollection();

            // Step 2: Register the services. Here we register a mock for ICalculatorSettingsService.
            //var mockSettingsService = new Mock<ICalculatorSettingsService>();
            //mockSettingsService.Setup(async service => await service.CalculateTaxAsync(new TaxCalculator {Income=0 });

            // Register the mock
            //services.AddSingleton(mockSettingsService.Object);

            // Register the real service
            services.AddScoped<IFlatRateCalculator, FlatRateCalculator>();
            services.AddScoped<ICalculatorSettingsService, CalculatorSettingsService>();

            // Build the ServiceProvider
            _serviceProvider = services.BuildServiceProvider();

            _flatRateCalculator = _serviceProvider.GetService<ICalculatorSettingsService>();
        }

        [TestCase(-1, 0)]
        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            var taxCalculator = new TaxCalculator { Calculator = CalculatorType.FlatRate, Income = income };

            // Act
            var tax = await new FlatRateCalculator(_flatRateCalculator).CalculateAsync(income);

            // Assert
            Assert.AreEqual(tax.Tax, expectedTax);
        }
    }
}