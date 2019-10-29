namespace Contract
{
    public interface IPayslipDetails
    {
        string Name { get; set; }
        string PayPeriod { get; set; }
        double GrossIncome { get; set;}
        double IncomeTax { get; set; }
        double NetIncome { get; }
        double Super { get; set; }
    }
    
    public class PayslipDetails : IPayslipDetails
    {
        public string Name { get; set; }
        public string PayPeriod { get; set; }
        public double GrossIncome { get; set; }
        public double IncomeTax { get; set; }
        public double NetIncome => GrossIncome - IncomeTax;
        public double Super { get; set; }
    }
}