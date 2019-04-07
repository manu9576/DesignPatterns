using Interfaces;
using Models;

namespace Visitors
{
    public class TaxableAmountVisitor : IVisitor
    {
        public double TaxableAmount
        {
            get;
            set;
        }

        public void Visit(MonthlySalary_Earning monthlySalary_Earning)
        {
            TaxableAmount += (monthlySalary_Earning.BasicSalary + monthlySalary_Earning.HRAExemption + monthlySalary_Earning.MedicalAllowance + monthlySalary_Earning.PersonalAllowance);
        }

        public void Visit(MonthlySalary_Deduction monthlySalary_Deduction)
        {
            TaxableAmount -= (monthlySalary_Deduction.ProvidentFund_EmployeeContribution + monthlySalary_Deduction.ProvidentFund_EmployerContribution + monthlySalary_Deduction.ProfessionTax + monthlySalary_Deduction.OtherDeduction);
        }

        public void Visit(MonthlyExpense monthlyExpense)
        {
            TaxableAmount -= monthlyExpense.MonthlyRent;
        }

        public void Visit(AnnualInvestment annualInvestment)
        {
            TaxableAmount -= annualInvestment.InvestmentAmmount;
        }
    }

}