using AutoFixture;
using BL;
using Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace UT.BL
{
    public class BlBaseTest : BaseTest
    {
        protected EmployeeDetails EmployeeDetails { get; set; }
        protected IOptions<TaxTable> TaxTable { get; set; }
        protected ILogger<ITaxYearManager> Logger { get; set; }


        [SetUp]
        public void Setup()
        {
            base.SetUp();
            EmployeeDetails = BaseFixture.Create<EmployeeDetails>();
            TaxTable = BaseFixture.Create<IOptions<TaxTable>>();
            Logger = BaseFixture.Create<ILogger<ITaxYearManager>>();
        }
    }
}