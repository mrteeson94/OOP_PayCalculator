using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayCalculatorTemplate;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        // Creating object and file path reference to be used in the unit test
        public const string FileName = @"C:\Users\AKATY\OneDrive\Desktop\OOP project\Part3Application\OOP_PayCalculator\PayCalculatorTemplate\employee.csv";
        public List<PaySlip> importedPaySlip = CsvImporterPaySlip.ImportPaySlip(FileName).ToList();

        /// <summary>
        /// Unit test for paycalculator methods (gross & super calculation)
        /// </summary>

        [TestMethod]
        public void CalculationUnitTest()
        {
            int hourlyRate = 25;
            double hoursWorked = 39;
            double superRate = 0.105;
            double controlGross = Math.Round(hourlyRate * hoursWorked, 2);
            double controlSuper = Math.Round(controlGross*superRate, 2);

            Assert.AreEqual(controlGross, PayCalculator.CalculateGrossPay(hourlyRate, hoursWorked));
            Assert.AreEqual(controlSuper, PayCalculator.CalculateSuper(controlGross,superRate));
        }
        
        /// <summary>
        /// Check if csv file being read in is null
        /// </summary>
        [TestMethod]
        public void importRecordsNotNull()
        {
            Assert.IsNotNull(importedPaySlip);
        }

        /// <summary>
        /// Integration test on CsvHelper, method checks if 1st row being read matches control list
        /// </summary>
        [TestMethod]
        public void ComponentIntegrationTest()
        {
            var controlList = new PaySlip();
            {
                controlList.Id = 1;
                controlList.FirstName = "Marge";
                controlList.LastName = "Larkin";
                controlList.TypeEmployee = "Employee";
                controlList.HourlyRate = 25;
                controlList.HasTaxThreshold = "Y";

                Assert.AreEqual(importedPaySlip[0].Id, controlList.Id);
                Assert.AreEqual(importedPaySlip[0].FirstName, controlList.FirstName);
                Assert.AreEqual(importedPaySlip[0].LastName, controlList.LastName);
                Assert.AreEqual(importedPaySlip[0].TypeEmployee, controlList.TypeEmployee);
                Assert.AreEqual(importedPaySlip[0].HourlyRate, controlList.HourlyRate);
                Assert.AreEqual(importedPaySlip[0].HasTaxThreshold, controlList.HasTaxThreshold);
            }
        }

    }
}
