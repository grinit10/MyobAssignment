using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Extensions;
using BL;
using Contract;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moq;
using NUnit.Framework;
using UT.BL;

namespace UT.API
{
    public class PayslipApiTest : BlBaseTest
    {
        private IPayslipManager _payslipManager;
        private ICsvDal<EmployeeDetails> _empCsvDal;
        private ICsvDal<PayslipDetails> _payCsvDal;
        private IFormFile _file;
        private PayslipController _payslipController;
        
        [SetUp]
        public new void Setup()
        {
            base.SetUp();
            
            var mockPayslipManager = new Mock<IPayslipManager>();
            mockPayslipManager.Setup(x => x.GeneratePayslip(EmployeeDetails)).Returns(new PayslipDetails()
            {
                Name = EmployeeDetails.FirstName + " " + EmployeeDetails.LastName,
                Super = 450,
                GrossIncome = 5004,
                IncomeTax = 922,
                PayPeriod = EmployeeDetails.PaymentStartDate
            });
            _payslipManager = mockPayslipManager.Object;
            
            var mockEmpCsvDal = new Mock<ICsvDal<EmployeeDetails>>();
            mockEmpCsvDal.Setup(x => x.ReadCsv(It.IsAny<IFormFile>())).Returns(new List<EmployeeDetails>()
            {
                EmployeeDetails,
                new EmployeeDetails()
                {
                    FirstName = EmployeeDetails.FirstName,
                    LastName = EmployeeDetails.LastName,
                    AnnualSalary = 120000,
                    SuperRate = 10,
                    PaymentStartDate = EmployeeDetails.PaymentStartDate
                }
            });
            _empCsvDal = mockEmpCsvDal.Object;
            
            var mockPayCsvDal = new Mock<ICsvDal<PayslipDetails>>();
            mockPayCsvDal.Setup(x => x.WriteCsv(It.IsAny<IEnumerable<PayslipDetails>>())).Returns(new byte[10]);
            _payCsvDal = mockPayCsvDal.Object;
            
            var fileMock = new Mock<IFormFile>();
            _file = fileMock.Object;
            
            _payslipController = new PayslipController(_payslipManager, _empCsvDal, _payCsvDal);
            var objectValidator = new Mock<IObjectModelValidator>();
            objectValidator.Setup(o => o.Validate(It.IsAny<ActionContext>(), 
                It.IsAny<ValidationStateDictionary>(), 
                It.IsAny<string>(), 
                It.IsAny<object>()));
            _payslipController.ObjectValidator = objectValidator.Object;
        }

        [Test]
        public async Task PayslipIsGeneratedAsync()
        {
            var response = (FileResult)_payslipController.ProcessFiles(_file);
            Assert.AreEqual("Payslips.csv",response.FileDownloadName);
            Assert.AreEqual("application/csv",response.ContentType);
        }
        
        [Test]
        public async Task PayslipValidationFailedAsync()
        {
            _payslipController.ModelState.AddModelError("AnnualSalary", "Please enter AnnualSalary greater than 0.");
            var response = (ValidationFailedResult)_payslipController.ProcessFiles(_file);
        }
    }
}