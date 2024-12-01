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
        [SetUp]
        public void Setup()
        {
 
        }

        [TestCase(-1, 0)]
        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange

            // Act
            
            // Assert

        }
    }
}