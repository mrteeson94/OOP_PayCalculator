using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PayCalculatorTemplate
{

    /// <summary>
    /// Base parent class to hold all Pay calculation functions for employees 
    /// Its properties align with threshold csv file that is called in from the child classes to perform tax calculation
    /// </summary>
    public class PayCalculator
        {
            public int IncomeRangeA { get; set; }
            public int IncomeRangeB { get; set; }
            public double TaxRateA { get; set; }
            public double TaxRateB { get; set; }


            /// <summary>
            /// Class method calculates gross income per employee
            /// </summary>
            /// <param name="hourlyRate"></param>
            /// <param name="hourWorked"></param>
            /// <returns>returns gross pay as a double data type</returns>
            public static double CalculateGrossPay(int hourlyRate, double hourWorked)
            {
                double grossPay = Math.Round(hourlyRate * hourWorked, 2);
                return grossPay;
            }
            /// <summary>
            /// Class method calculates super amount per employee
            /// </summary>
            /// <param name="grossPay"></param>
            /// <param name="superRate"></param>
            /// <returns>returns super amount as a double data type </returns>
            public static double CalculateSuper(double grossPay, double superRate)
                    {
                        double superAmount = Math.Round(grossPay * superRate, 2);
                        return superAmount;
                    }
            /// <summary>
            /// Class method calculates net pay per employee
            /// </summary>
            /// <param name="grossPay"></param>
            /// <param name="superAmount"></param>
            /// <param name="taxAmount"></param>
            /// <returns>returns net pay pay as a double data type</returns>
            public static double CalculateNetPay(double grossPay, double superAmount, double taxAmount)
                    {
                        double netPay = Math.Round(grossPay - (superAmount + taxAmount),2);
                        return netPay;
                    }
        }
}