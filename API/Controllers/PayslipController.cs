using System;
using System.Collections.Generic;
using System.Linq;
using API.Extensions;
using BL;
using Contract;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(CustomLoggingExceptionFilter))]
    [Route("[controller]")]
    [ValidateModelAttribute]
    public class PayslipController : ControllerBase
    {
        private readonly IPayslipManager _payslipManager;
        private readonly ICsvDal<EmployeeDetails> _empCsvDal;
        private readonly ICsvDal<PayslipDetails> _payCsvDal;

        public PayslipController(IPayslipManager payslipManager, ICsvDal<EmployeeDetails> empCsvDal, ICsvDal<PayslipDetails> payCsvDal)
        {
            _payslipManager = payslipManager;
            _empCsvDal = empCsvDal;
            _payCsvDal = payCsvDal;
        }

        [HttpPost]
        public IActionResult ProcessFiles(IFormFile file)
        {
            IList<PayslipDetails> payslips = new List<PayslipDetails>();
            _empCsvDal.ReadCsv(file).ToList().ForEach(e =>
            {
                if (TryValidateModel(e))
                {
                    payslips.Add(_payslipManager.GeneratePayslip(e));
                }
            });
            if(!ModelState.IsValid)
                return new ValidationFailedResult(ModelState);
            return File(_payCsvDal.WriteCsv(payslips), "application/csv", "Payslips.csv");
        }
    }
}