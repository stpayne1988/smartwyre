using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Incentives;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void FixedCashAmountCalculator_CalculatesExpectedAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 15m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
        var request = new CalculateRebateRequest { Volume = 1 };

        var calc = new FixedCashAmountCalculator();

        // Act
        var result = calc.Calculate(rebate, product, request);

        // Assert
        Assert.True(calc.IsValid(rebate, product, request));
        Assert.Equal(15m, result);
    }

    [Fact]
    public void FixedRateRebateCalculator_CalculatesExpectedAmount()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { Price = 20m, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };
        var request = new CalculateRebateRequest { Volume = 2 };

        var calc = new FixedRateRebateCalculator();

        // Act
        var result = calc.Calculate(rebate, product, request);

        // Assert
        Assert.True(calc.IsValid(rebate, product, request));
        Assert.Equal(4m, result);
    }

    [Fact]
    public void AmountPerUomCalculator_CalculatesExpectedAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 3m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };
        var request = new CalculateRebateRequest { Volume = 5 };

        var calc = new AmountPerUomCalculator();

        // Act
        var result = calc.Calculate(rebate, product, request);

        // Assert
        Assert.True(calc.IsValid(rebate, product, request));
        Assert.Equal(15m, result);
    }

    [Fact]
    public void RebateService_ValidRequest_PersistsCalculation()
    {
        // Arrange
        var rebate = new Rebate { Identifier = "R1", Incentive = IncentiveType.FixedCashAmount, Amount = 10m };
        var product = new Product { Identifier = "P1", SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        var rebateRepo = new FakeRebateRepository(rebate);
        var productRepo = new FakeProductRepository(product);
        var factory = new IncentiveCalculatorFactory(new FixedCashAmountCalculator(), new FixedRateRebateCalculator(), new AmountPerUomCalculator());

        var service = new RebateService(rebateRepo, productRepo, factory);

        var request = new CalculateRebateRequest { RebateIdentifier = "R1", ProductIdentifier = "P1", Volume = 1 };

        // Act
        var result = service.Calculate(request);

        // Assert
        Assert.True(result.Success);
        Assert.True(rebateRepo.SaveCalled);
        Assert.Equal(10m, rebateRepo.SavedAmount);
    }

    class FakeRebateRepository : IRebateRepository
    {
        private readonly Rebate _rebate;
        public bool SaveCalled { get; private set; }
        public decimal SavedAmount { get; private set; }

        public FakeRebateRepository(Rebate rebate)
        {
            _rebate = rebate;
        }

        public Rebate GetRebate(string rebateIdentifier) => _rebate;

        public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
        {
            SaveCalled = true;
            SavedAmount = rebateAmount;
        }
    }

    class FakeProductRepository : IProductRepository
    {
        private readonly Product _product;
        public FakeProductRepository(Product product)
        {
            _product = product;
        }

        public Product GetProduct(string productIdentifier) => _product;
    }
}
