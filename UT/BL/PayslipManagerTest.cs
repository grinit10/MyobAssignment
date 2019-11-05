using System.Threading.Tasks;
using BL;
using Moq;
using NUnit.Framework;

namespace UT.BL
{
    public class PayslipManagerTest : BlBaseTest
    {
        private PayslipManager _PayslipManager { get; set; }

        [SetUp]
        public new void Setup()
        {
            base.SetUp();
            var taxYearManager = new Mock<ITaxYearManager>(); 
            taxYearManager.Setup(x => x.TaxDeduction(EmployeeDetails.AnnualSalary, 2019)).Returns(922);
            _PayslipManager = new PayslipManager(taxYearManager.Object);
        }

        /// <summary>Throws exception when trying to calculate tax of a year which has not yet started.</summary>
        [Test]
        public async Task GeneratePayslipCorrectlyTask()
        {
            var payslip = _PayslipManager.GeneratePayslip(EmployeeDetails);
            Assert.AreEqual(EmployeeDetails.FirstName + " " + EmployeeDetails.LastName, payslip.Name);
            Assert.AreEqual(450,payslip.Super);
            Assert.AreEqual(5004, payslip.GrossIncome);
            Assert.AreEqual(4082, payslip.NetIncome);
            Assert.AreEqual(EmployeeDetails.PaymentStartDate, payslip.PayPeriod);
        }
    }
}