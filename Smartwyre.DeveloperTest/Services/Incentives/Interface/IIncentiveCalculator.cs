using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Incentives;

public interface IIncentiveCalculator
{
    bool IsValid(Rebate rebate, Product product, CalculateRebateRequest request);
    decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
}
