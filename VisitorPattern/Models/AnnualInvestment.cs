using Interfaces;

namespace Models
{
    public class AnnualInvestment : ISalary
    {
        public string InvestmentDetails
        {
            get;
            set;
        }

        public double InvestmentAmmount
        {
            get;
            set;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}