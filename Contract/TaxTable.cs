using System.Collections.Generic;

namespace Contract
{
    public class TaxTable
    {
        public TaxTable()
        {
            TaxYears = new List<TaxYear>();
        }

        public IList<TaxYear> TaxYears { get; private set; }
    }

    public class TaxYear
    {
        public TaxYear()
        {
            TaxSlabs = new List<TaxSlab>();
        }

        public int Year { get; private set; }
        public IList<TaxSlab> TaxSlabs { get; private set; }
    }
    public class TaxSlab
    {
        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
        public double Base { get; set; }
        public double Rate { get; set; }
    }
}