using System;

namespace ClassLibraryTSQLProcedure1
{
    using System.Data.SqlTypes;

    public class BudgetPercent
    {
        private const float percent = 12;

        public static SqlDouble ComputeBudget(float budget)
        {
            return budget * percent;
        }
    }
}
