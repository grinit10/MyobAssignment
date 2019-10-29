using API.Extensions;
using BL;
using Contract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(CustomLoggingExceptionFilter))]
    [Route("[controller]")]
    public class PayslipController : ControllerBase
    {
        private readonly IPayslipManager _payslipManager;

        public PayslipController(IPayslipManager payslipManager)
        {
            _payslipManager = payslipManager;
        }

        [HttpGet("HealthCheck")]
        public IActionResult Get()
        {
            return Ok("It is healthy");
        }

        [HttpGet]
        public IActionResult Get(string firstName, string lastName, double annualSalary, double superRate,
            string paymentStartDate)
        {
            var empDetails = new EmployeeDetails()
            {
                FirstName = firstName,
                LastName = lastName,
                AnnualSalary = annualSalary,
                SuperRate = superRate,
                PaymentStartDate = paymentStartDate
            };
            return TryValidateModel(empDetails) ? Ok(_payslipManager.GeneratePayslip(empDetails)) : ValidationProblem();
        }
    }
}