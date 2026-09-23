using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Incentives;

public class AmountPerUomCalculator : IIncentiveCalculator
{
    public bool IsValid(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        if (rebate == null)
        {
            return false;
        }

        if (product == null)
        {
            return false;
        }

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            return false;
        }

        if (rebate.Amount == 0 || request.Volume == 0)
        {
            return false;
        }

        return true;
    }

    public decimal Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
    {
        return rebate.Amount * request.Volume;
    }
}
