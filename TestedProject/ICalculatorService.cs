using System;

namespace TestedProject
{
    public interface ICalculatorService
    {
        decimal GetGrossFromNet(decimal net, decimal tax);
        DateTime StartPeriodDate(DateTime dateTime);
    }
}