using System;
using Contract;

namespace BL
{
    public class PayslipManager : IPayslipManager
    {
        private readonly ITaxYearManager _taxYearManager;

        public PayslipManager(ITaxYearManager taxYearManager)
        {
            _taxYearManager = taxYearManager;
        }

        public IPayslipDetails GeneratePayslip(IEmployeeDetails employeeDetails) => new PayslipDetails()
        {
            PayPeriod = employeeDetails.PaymentStartDate,
            Name = $"{employeeDetails.FirstName} {employeeDetails.LastName}",
            GrossIncome = Convert.ToInt32(employeeDetails.AnnualSalary / 12),
            Super = Convert.ToInt32(employeeDetails.AnnualSalary * employeeDetails.SuperRate / 1200),
            IncomeTax = Convert.ToInt32(_taxYearManager.TaxDeduction(employeeDetails.AnnualSalary)),
        };
    }
}