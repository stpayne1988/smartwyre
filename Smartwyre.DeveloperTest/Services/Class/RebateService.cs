using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services.Incentives;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateRepository _rebateRepository;
    private readonly IProductRepository _productRepository;
    private readonly IIncentiveCalculatorFactory _incentiveCalculatorFactory;

    public RebateService()
    {
        _rebateRepository = new RebateDataStore();
        _productRepository = new ProductDataStore();
        _incentiveCalculatorFactory = new IncentiveCalculatorFactory();
    }

    // Allow dependencies to be injected for testing
    public RebateService(IRebateRepository rebateRepository, IProductRepository productRepository, IncentiveCalculatorFactory incentiveCalculatorFactory)
    {
        _rebateRepository = rebateRepository;
        _productRepository = productRepository;
        _incentiveCalculatorFactory = incentiveCalculatorFactory;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        CalculateRebateResult result = new();

        Rebate rebate = _rebateRepository.GetRebate(request.RebateIdentifier);
        Product product = _productRepository.GetProduct(request.ProductIdentifier);

        if (rebate == null)
        {
            result.Success = false;
            return result;
        }

        var calculator = _incentiveCalculatorFactory.GetCalculator(rebate.Incentive);
        if (calculator == null)
        {
            result.Success = false;
            return result;
        }

        if (!calculator.IsValid(rebate, product, request))
        {
            result.Success = false;
            return result;
        }

        var rebateAmount = calculator.Calculate(rebate, product, request);

        _rebateRepository.StoreCalculationResult(rebate, rebateAmount);

        result.Success = true;
        return result;
    }
}
