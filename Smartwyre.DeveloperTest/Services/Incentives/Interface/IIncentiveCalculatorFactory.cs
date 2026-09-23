using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Incentives;

public interface IIncentiveCalculatorFactory
{
    IIncentiveCalculator GetCalculator(IncentiveType type);
}
