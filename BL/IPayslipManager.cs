using Contract;

namespace BL
{
    public interface IPayslipManager
    {
        IPayslipDetails GeneratePayslip(IEmployeeDetails employeeDetails);
    }
}