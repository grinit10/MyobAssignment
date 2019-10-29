namespace BL
{
    public interface ITaxYearManager
    {
        double TaxDeduction(double salary, int year = 2019);
    }
}