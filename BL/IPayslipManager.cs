using Contract;

namespace BL
{
    public interface IPayslipManager
    {
        PayslipDetails GeneratePayslip(IEmployeeDetails employeeDetails);
    }
}