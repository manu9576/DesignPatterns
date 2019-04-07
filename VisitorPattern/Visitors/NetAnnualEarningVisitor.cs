using Interfaces;
using Models;

namespace Visitors
{
    public class NetAnnualEarningVisitor : IVisitor
    {
        public double NetEarningoftheYear
        {
            get;
            set;
        }

        public void Visit(MonthlySalary_Earning monthlySalary_Earning)
        {
            NetEarningoftheYear += (monthlySalary_Earning.BasicSalary + monthlySalary_Earning.ConveyanceAllowance + monthlySalary_Earning.FoodCard_Bill + monthlySalary_Earning.HRAExemption + monthlySalary_Earning.MedicalAllowance + monthlySalary_Earning.OtherBills + monthlySalary_Earning.PersonalAllowance + monthlySalary_Earning.TelephoneBill);
        }

        public void Visit(MonthlySalary_Deduction monthlySalary_Deduction)
        {
            NetEarningoftheYear -= (monthlySalary_Deduction.ProvidentFund_EmployeeContribution + monthlySalary_Deduction.ProvidentFund_EmployerContribution + monthlySalary_Deduction.ProfessionTax + monthlySalary_Deduction.OtherDeduction);
        }

        public void Visit(AnnualInvestment annualInvestment)
        {
            // do nothing  
        }

        public void Visit(MonthlyExpense monthlyExpense)
        {
            //do nothing  
        }
    }
}