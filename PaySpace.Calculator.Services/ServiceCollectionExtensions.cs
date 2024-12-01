using Microsoft.Extensions.DependencyInjection;

using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;
using PaySpace.Calculator.Services.Factory;

namespace PaySpace.Calculator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorServices(this IServiceCollection services)
        {
            services.AddScoped<IPostalCodeService, PostalCodeService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<ICalculatorSettingsService, CalculatorSettingsService>();

            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddScoped<ICalculatorFactory, CalculatorFactory>();

            services.AddMemoryCache();
        }
    }
}