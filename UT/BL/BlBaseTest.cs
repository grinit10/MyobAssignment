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
            EmployeeDetails.AnnualSalary = 60050;
            EmployeeDetails.SuperRate = 9;
            var taxTable = new TaxTable()
            {
                TaxYears =
                {
                    new TaxYear()
                    {
                        Year = 2019,
                        TaxSlabs =
                        {
                            new TaxSlab()
                            {
                                Base = 0,
                                Rate = 0,
                                LowerLimit = 0,
                                UpperLimit = 18200
                            },
                            new TaxSlab()
                            {
                                Base = 0,
                                Rate = 19,
                                LowerLimit = 18201,
                                UpperLimit = 37000
                            },
                            new TaxSlab()
                            {
                                Base = 3572,
                                Rate = 32.5,
                                LowerLimit = 37001,
                                UpperLimit = 87000
                            },
                            new TaxSlab()
                            {
                                Base = 19822,
                                Rate = 37,
                                LowerLimit = 87001,
                                UpperLimit = 180000
                            },
                            new TaxSlab()
                            {
                                Base = 54232,
                                Rate = 45,
                                LowerLimit = 180001,
                                UpperLimit = 1000000
                            }
                        }
                    }
                }
            };
            TaxTable = Options.Create(taxTable);
            Logger = BaseFixture.Create<ILogger<ITaxYearManager>>();
        }
    }
}