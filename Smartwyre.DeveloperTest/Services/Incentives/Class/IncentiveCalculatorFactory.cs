using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Incentives;

public class IncentiveCalculatorFactory : IIncentiveCalculatorFactory
{
    private readonly FixedCashAmountCalculator _fixedCash;
    private readonly FixedRateRebateCalculator _fixedRate;
    private readonly AmountPerUomCalculator _amountPerUom;

    public IncentiveCalculatorFactory(FixedCashAmountCalculator fixedCash, FixedRateRebateCalculator fixedRate, AmountPerUomCalculator amountPerUom)
    {
        _fixedCash = fixedCash;
        _fixedRate = fixedRate;
        _amountPerUom = amountPerUom;
    }

    // default parameterless ctor kept for backward compatibility
    public IncentiveCalculatorFactory()
    {
        _fixedCash = new FixedCashAmountCalculator();
        _fixedRate = new FixedRateRebateCalculator();
        _amountPerUom = new AmountPerUomCalculator();
    }

    public IIncentiveCalculator GetCalculator(IncentiveType type)
    {
        return type switch
        {
            IncentiveType.FixedCashAmount => _fixedCash,
            IncentiveType.FixedRateRebate => _fixedRate,
            IncentiveType.AmountPerUom => _amountPerUom,
            _ => null
        };
    }
}
