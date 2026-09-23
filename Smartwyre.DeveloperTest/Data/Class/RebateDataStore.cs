using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateRepository
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve rebate, code removed for brevity 
        return new Rebate();
    }

    public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
    {
        // Update rebate in database, code removed for brevity
    }
}
