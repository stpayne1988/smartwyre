using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IRebateRepository
{
    Rebate GetRebate(string rebateIdentifier);
    void StoreCalculationResult(Rebate rebate, decimal rebateAmount);
}
