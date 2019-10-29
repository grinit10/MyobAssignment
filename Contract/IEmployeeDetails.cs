using System.ComponentModel.DataAnnotations;

namespace Contract
{
    public interface IEmployeeDetails
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        double AnnualSalary { get; set; }
        double SuperRate { get; set; }
        string PaymentStartDate { get; set; }
    }
    
    public class EmployeeDetails : IEmployeeDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Please enter AnnualSalary greater than 0")]
        public double AnnualSalary { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter super rate between 0% and 50%")]
        public double SuperRate { get; set; }
        public string PaymentStartDate { get; set; }
    }
}