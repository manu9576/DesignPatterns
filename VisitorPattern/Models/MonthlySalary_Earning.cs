using Interfaces;

namespace Models
{
    public class MonthlySalary_Earning : ISalary
    {
        public string MonthName
        {
            get;
            set;
        }

        public double BasicSalary
        {
            get;
            set;
        }

        public double HRAExemption
        {
            get;
            set;
        }

        public double ConveyanceAllowance
        {
            get;
            set;
        }

        public double PersonalAllowance
        {
            get;
            set;
        }

        public double MedicalAllowance
        {
            get;
            set;
        }

        public double TelephoneBill
        {
            get;
            set;
        }

        public double FoodCard_Bill
        {
            get;
            set;
        }

        public double OtherBills
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