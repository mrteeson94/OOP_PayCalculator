using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayCalculatorTemplate;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace PayCalculatorTemplate
{
    internal class CsvImporterPaySlip
    {
        /// <summary>
        /// Class for importing employee csv and storing list of employees via myRecords
        /// <param name="FileName">stores file path for employee.csv</param>
        /// <returns>A list of employees detail containing id, firstname, lastname, typeemployee, hourlyrate and taxthresholdstatus </returns>
        /// </summary>
        public static List<PaySlip> ImportPaySlip(string FileName)
        {
            var myRecords = new List<PaySlip>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using (var reader = new StreamReader(FileName))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<PaySlipMap>();
                    var records = csv.GetRecords<PaySlip>();
                    myRecords = records.ToList(); //adds all the rows to myRecords
                }
            }
            return myRecords;

        }
        /// <summary>
        /// Class for importing threshold csv and storing list of employee's income range & tax rates via myRecords
        /// <param name="FileName">stores file path for taxrate-nothreshold.csv OR taxrate-withthreshold.csv</param>
        /// <returns>A list of tax details containing income range start, income range end, taxrateA, and taxrateB </returns>
        /// </summary>
        public static List<PayCalculator> ImportPayCalculator(string FileName)
        {
            var myRecords = new List<PayCalculator>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using (var reader = new StreamReader(FileName))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<PayCalculatorMap>();
                    var records = csv.GetRecords<PayCalculator>();
                    myRecords = records.ToList(); //adds all the rows to myRecords
                }
            }
            return myRecords;

        }

        /// <summary>
        /// subClass inheriting from Payslip class to map out properties with the csv values being read in from employee csv 
        /// </summary>
        public class PaySlipMap : ClassMap<PaySlip>
        {
            public PaySlipMap()
            {
                Map(m => m.Id).Index(0);
                Map(m => m.FirstName).Index(1);
                Map(m => m.LastName).Index(2);
                Map(m => m.TypeEmployee).Index(3);
                Map(m => m.HourlyRate).Index(4);
                Map(m => m.HasTaxThreshold).Index(5);
            }
        }
        /// <summary>
        /// subClass inheriting from PayCalculator class to map out properties with the csv values being read in from threshold csv files 
        /// </summary>
        public class PayCalculatorMap : ClassMap<PayCalculator>
        {
            public PayCalculatorMap()
            {
                Map(m => m.IncomeRangeA).Index(0);
                Map(m => m.IncomeRangeB).Index(1);
                Map(m => m.TaxRateA).Index(2);
                Map(m => m.TaxRateB).Index(3);
            }
        }


    }
}
