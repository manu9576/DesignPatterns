using Models;

namespace Interfaces
{
    public interface IVisitor
    {
        void Visit(MonthlySalary_Earning monthlySalary_Earning);

        void Visit(MonthlySalary_Deduction monthlySalary_Deduction);

        void Visit(MonthlyExpense monthlyExpense);

        void Visit(AnnualInvestment annualInvestment);

    }
}