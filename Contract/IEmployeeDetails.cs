using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

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
        [Index(0)]
        public string FirstName { get; set; }
        [Index(1)]
        public string LastName { get; set; }
        [Index(2)]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter AnnualSalary greater than 0")]
        public double AnnualSalary { get; set; }
        [Index(3)]
        [Range(0, 50, ErrorMessage = "Please enter super rate between 0% and 50%")]
        public double SuperRate { get; set; }
        [Index(4)]
        public string PaymentStartDate { get; set; }
    }
}