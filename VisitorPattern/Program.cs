using Models;
using System;
using System.Globalization;
using Visitors;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee
            {
                Id = "XYZ1001",
                Name = "Banketeshvar Narayan"
            };

            AddDataForEmployee(emp);

            Console.WriteLine("Execution without visitor:");
            ExecutionWithoutVisitor(emp);

            Console.WriteLine("Execution with visitor:");
            ExecutionVisitors(emp);

            Console.ReadKey();
        }

        private static void ExecutionVisitors(Employee emp)
        {
            NetAnnualEarningVisitor netAnnualEarningVisitor = new NetAnnualEarningVisitor();
            emp.Accept(netAnnualEarningVisitor);

            TaxableAmountVisitor annualTaxableAmount = new TaxableAmountVisitor();
            emp.Accept(annualTaxableAmount);

            Console.WriteLine("Annual Net Earning Amount : {0}", netAnnualEarningVisitor.NetEarningoftheYear);
            Console.WriteLine("Annual Taxable Amount : {0}", annualTaxableAmount.TaxableAmount);
        }

        private static void ExecutionWithoutVisitor(Employee emp)
        {
            double NetEarningoftheYear = 0.0;
            double TaxableAmount = 0.0;

            foreach (var salary in emp.Salaries)
            {
                switch(salary.GetType().Name)
                {
                    case nameof(MonthlySalary_Earning):

                        var monthlySalary_Earning = salary as MonthlySalary_Earning;
                        NetEarningoftheYear += (monthlySalary_Earning.BasicSalary + monthlySalary_Earning.ConveyanceAllowance + monthlySalary_Earning.FoodCard_Bill + monthlySalary_Earning.HRAExemption + monthlySalary_Earning.MedicalAllowance + monthlySalary_Earning.OtherBills + monthlySalary_Earning.PersonalAllowance + monthlySalary_Earning.TelephoneBill);
                        TaxableAmount += (monthlySalary_Earning.BasicSalary + monthlySalary_Earning.HRAExemption + monthlySalary_Earning.MedicalAllowance + monthlySalary_Earning.PersonalAllowance);

                        break;

                    case nameof(MonthlySalary_Deduction):

                        var monthlySalary_Deduction = salary as MonthlySalary_Deduction;
                        NetEarningoftheYear -= (monthlySalary_Deduction.ProvidentFund_EmployeeContribution + monthlySalary_Deduction.ProvidentFund_EmployerContribution + monthlySalary_Deduction.ProfessionTax + monthlySalary_Deduction.OtherDeduction);
                        TaxableAmount -= (monthlySalary_Deduction.ProvidentFund_EmployeeContribution + monthlySalary_Deduction.ProvidentFund_EmployerContribution + monthlySalary_Deduction.ProfessionTax + monthlySalary_Deduction.OtherDeduction);

                        break;

                    case nameof(MonthlyExpense):

                        var monthlyExpense = salary as MonthlyExpense;
                        TaxableAmount -= monthlyExpense.MonthlyRent;

                        break;

                    case nameof(AnnualInvestment):

                        var annualInvestment = salary as AnnualInvestment;
                        TaxableAmount -= annualInvestment.InvestmentAmmount;

                        break;
                }

            }

            Console.WriteLine("Annual Net Earning Amount : {0}", NetEarningoftheYear);
            Console.WriteLine("Annual Taxable Amount : {0}", TaxableAmount);
        }

        private static void AddDataForEmployee(Employee emp)
        {
            for (int i = 1; i <= 12; i++)
            {
                emp.Salaries.Add(new MonthlySalary_Earning
                {
                    MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                    BasicSalary = 120000,
                    HRAExemption = 50000,
                    ConveyanceAllowance = 1600,
                    PersonalAllowance = 45000,
                    MedicalAllowance = 1500,
                    TelephoneBill = 2500,
                    FoodCard_Bill = 3000,
                    OtherBills = 35000
                });
                emp.Salaries.Add(new MonthlySalary_Deduction
                {
                    MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                    ProvidentFund_EmployeeContribution = 8000,
                    ProvidentFund_EmployerContribution = 8000,
                    OtherDeduction = 700,
                    ProfessionTax = 200,
                    TDS = 15000
                });
                emp.Salaries.Add(new MonthlyExpense
                {
                    MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(1),
                    MonthlyRent = 10000
                });
            }
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "MediclaimPolicy",
                InvestmentAmmount = 15000
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "MediclaimPolicyforParents",
                InvestmentAmmount = 25000
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "HouseLoan",
                InvestmentAmmount = 0.0
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "EducationLoan",
                InvestmentAmmount = 0.0
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "OtherInvestment",
                InvestmentAmmount = 5000
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "RGESS",
                InvestmentAmmount = 5500
            });
            emp.Salaries.Add(new AnnualInvestment
            {
                InvestmentDetails = "Section80Cn80CCD_ExceptPF",
                InvestmentAmmount = 100000
            });
        }
    }
}
