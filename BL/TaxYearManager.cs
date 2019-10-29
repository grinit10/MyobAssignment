using System.Linq;
using Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BL
{
    public class TaxYearManager : ITaxYearManager
    {
        private readonly IOptions<TaxTable> _taxTable;
        private readonly ILogger<ITaxYearManager> _logger;


        public TaxYearManager(IOptions<TaxTable> taxTable, ILogger<ITaxYearManager> logger)
        {
            _taxTable = taxTable;
            _logger = logger;
        }

        public double TaxDeduction(double salary, int year = 2019)
        {
            var selectedSlab = _taxTable.Value.TaxYears.First(ty => ty.Year == year)
                .TaxSlabs
                .First(ts => ts.LowerLimit <= salary && ts.UpperLimit >= salary);
            _logger.LogDebug($"Selected slab details: {selectedSlab}");
            return (selectedSlab.Base + (salary - selectedSlab.LowerLimit) * selectedSlab.Rate/100)/12;
        }
    }
}