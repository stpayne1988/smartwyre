using System;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Incentives;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Rebate Identifier:");
        var rebateId = Console.ReadLine();

        Console.WriteLine("Enter Product Identifier:");
        var productId = Console.ReadLine();

        Console.WriteLine("Enter Volume (number):");
        var volumeText = Console.ReadLine();
        decimal.TryParse(volumeText, out var volume);

        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateId,
            ProductIdentifier = productId,
            Volume = volume
        };

        // Setup DI
        var services = new ServiceCollection();
        services.AddSingleton<IRebateRepository, RebateDataStore>();
        services.AddSingleton<IProductRepository, ProductDataStore>();
        services.AddSingleton<FixedCashAmountCalculator>();
        services.AddSingleton<FixedRateRebateCalculator>();
        services.AddSingleton<AmountPerUomCalculator>();
        services.AddSingleton<IIncentiveCalculatorFactory, IncentiveCalculatorFactory>();
        services.AddTransient<IRebateService, RebateService>();

        var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<IRebateService>();
        var result = service.Calculate(request);

        Console.WriteLine($"Calculation Success: {result.Success}");
    }
}
