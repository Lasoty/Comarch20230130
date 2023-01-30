using System;

namespace TestedProject
{
    public class CalculatorService : ICalculatorService
    {
        public decimal GetGrossFromNet(decimal net, decimal tax)
        {
            if (tax < 0)
            {
                throw new ArgumentException("Wartość nie może być mniejsza niż 0.", nameof(tax));
            }
            return net * (1 + tax);
        }
    }
}
