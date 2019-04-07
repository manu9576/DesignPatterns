namespace Interfaces
{
    public interface ISalary
    {
        void Accept(IVisitor visitor);
    }
}