using System.Collections.Generic;
using System.Linq;

namespace PayCalculatorTemplate
{
    /// <summary>
    /// Child class of PayCalculator: Extends PayCalculator class handling With tax threshold in tax calculation
    /// </summary>
    public class GetTaxRatesFromGrossPay : PayCalculator
        {
            /// <summary>
            /// Finds 
            /// </summary>
            /// <param name="grossPay"></param>
            /// <returns></returns>
            public static double[] GetTaxRates(double grossPay, string filePath)
            {
                double[] taxRate = new double[2];

                //Read and assign object list to store the tax rate from taxrate-withthreshold.csv  
                List<PayCalculator> importedThreshold;
                importedThreshold = CsvImporterPaySlip.ImportPayCalculator(filePath).ToList();

                for (int i = 0; i <= importedThreshold.Count; i++)
                {
                    if (grossPay > importedThreshold[i].IncomeRangeA && grossPay < importedThreshold[i].IncomeRangeB)
                    {
                        taxRate[0] = importedThreshold[i].TaxRateA;
                        taxRate[1] = importedThreshold[i].TaxRateB;
                        return taxRate;
                    }
                }
                return taxRate;
            }
        }
}