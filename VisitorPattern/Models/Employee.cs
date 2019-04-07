using Interfaces;
using System.Collections.Generic;

namespace Models
{
    public class Employee
    {
        public string Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<ISalary> Salaries = new List<ISalary>();

        public void Accept(IVisitor visitor)
        {
            foreach (ISalary salary in Salaries)
            {
                salary.Accept(visitor);
            }
        }
    }

}