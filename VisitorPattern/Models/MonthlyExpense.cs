using Interfaces;

namespace Models
{
    public class MonthlyExpense : ISalary
    {
        public string MonthName
        {
            get;
            set;
        }

        public double MonthlyRent
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