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

        /// <summary>Returns the empty when no data returned from API asynchronous.</summary>
        /// <returns></returns>
        [Test]
        public async Task ThrowExceptionWhenNoMatchingYearAsync()
        {
            Assert.Throws<NullReferenceException>(() =>
                _taxYearManager.TaxDeduction(60000,
                    TaxTable.Value.TaxYears.FirstOrDefault().Year + 1));
        }
    }
}