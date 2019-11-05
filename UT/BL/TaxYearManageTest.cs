using System;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Moq;
using NUnit.Framework;

namespace UT.BL
{
    public class TaxYearManageTest : BlBaseTest
    {
        private TaxYearManager _taxYearManager { get; set; }

        [SetUp]
        public new void Setup()
        {
            base.SetUp();
            _taxYearManager = new TaxYearManager(TaxTable, Logger);
        }

        /// <summary>Throws exception when trying to calculate tax of a year which has not yet started.</summary>
        [Test]
        public async Task ThrowExceptionWhenNoMatchingYearAsync()
        {
            Assert.Throws<InvalidOperationException>(() =>
                _taxYearManager.TaxDeduction(EmployeeDetails.AnnualSalary,
                    TaxTable.Value.TaxYears.FirstOrDefault().Year + 1));
        }
        
        /// <summary>Returns correct tax deductions.</summary>
        [Test]
        public async Task CalculateCorrectTaxAmount()
        {
            var taxAmount = Convert.ToInt32(_taxYearManager.TaxDeduction(EmployeeDetails.AnnualSalary,
                TaxTable.Value.TaxYears.FirstOrDefault().Year));
            Assert.AreEqual(922, taxAmount );
        }
    }
}