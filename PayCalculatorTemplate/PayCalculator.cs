using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCalculatorTemplate
{

    /// <summary>
    /// Sets properties of employee's pay slip record detail
    /// Also sets properties for final calculation value to be mapped out when exporting csv file
    /// </summary>
    public class PaySlip
    {
        
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? TypeEmployee { get; set; }
        public int HourlyRate { get; set; }
        public string? HasTaxThreshold { get; set; }
        //properties to be used for btn_save
        public double GrossPay { get; set;}
        public double TaxAmount { get; set;}
        public double SuperAmount { get; set; }
        public double NetPay { get; set; }
    }

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

        /// <summary>
        /// Child class of PayCalculator: Extends PayCalculator class handling With tax threshold in tax calculation
        /// </summary>
        public class PayCalculatorWithThreshold : PayCalculator
        {
            /// <summary>
            /// Finds 
            /// </summary>
            /// <param name="grossPay"></param>
            /// <returns></returns>
            public static double[] CalculateTax(double grossPay)
            {
                double[] taxRate = new double[2];

                //Read and assign object list to store the tax rate from taxrate-withthreshold.csv  
                string FileName = @"C:\Users\AKATY\source\repos\OOP_PayCalculator\PayCalculatorTemplate\taxrate-withthreshold.csv";
                List<PayCalculator> importedThreshold = new List<PayCalculator>();
                importedThreshold = CsvImporterPaySlip.ImportPayCalculator(FileName).ToList();

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

    /// <summary>
    /// Child class of PayCalculator: Extends PayCalculator class handling no tax threshold in tax calculation
    /// </summary>
    public class PayCalculatorNoThreshold : PayCalculator
        {
            /// <summary>
            /// checks with given gross pay amount of what tax range for the employee to recieve 
            /// </summary>
            /// <param name="grossPay"></param>
            /// <returns>retruns taxrateA and taxrateB in double array struct</returns>
            public static double[] CalculateTax(double grossPay)
            {
                double[] taxRate = new double[2];
                //Read and assign object list to store the tax rate from taxrate-nothreshold.csv
                string FileName = @"C:\Users\AKATY\source\repos\OOP_PayCalculator\PayCalculatorTemplate\taxrate-nothreshold.csv";
                List<PayCalculator> importedThreshold = new List<PayCalculator>();
                importedThreshold = CsvImporterPaySlip.ImportPayCalculator(FileName).ToList();

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