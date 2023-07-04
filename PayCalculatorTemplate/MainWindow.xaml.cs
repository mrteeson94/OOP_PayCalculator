using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PayCalculatorTemplate;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;

namespace PayCalculatorTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public const string FileName = @"C:\Users\AKATY\source\repos\OOP_PayCalculator\PayCalculatorTemplate\rawData\employee.csv";
        //instantiate List<object> to store employee payslip details
        List<PaySlip> importedRecords = CsvImporterPaySlip.ImportPaySlip(FileName);
        //hoursworked to store user input in textbox
        int hoursWorked;
        //superRate is defined outside of the payslip && paycalculator as its defined by a fixed value currently 10.5%
        double superRate = 0.105;
        //instantiate List<object> to store calculations per employee
        List<PaySlip> list = new List<PaySlip>();


        /// <summary>
        /// Initiates window WPF application with employee detail datagrid filled with employee details from employee.csv
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            empDataGrid.DataContext = importedRecords;
        }


        /// <summary>
        /// Calls all calculation methods and displays on the payment summary datagrid on the WPF UI window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_click_calculate(object sender, RoutedEventArgs e)
        {
            hoursWorked = Convert.ToInt32(TextBoxHours.Text);
            //MessageBox.Show($"user test input: {hoursWorked}hrs");  <-- alert message showing user input

            //calculation logic

            importedRecords = CsvImporterPaySlip.ImportPaySlip(FileName).ToList();
            double grossPay;
            double taxAmount;
            double superAmount;
            double netPay;

            //loop through and store calculation + employee payslip detail into list object
            foreach (var paySlip in importedRecords)
            {
                //Calculate the gross and tax amounts
                double[] taxRate = new double[2];
                grossPay = PayCalculator.CalculateGrossPay(paySlip.HourlyRate, hoursWorked);

                if (paySlip.HasTaxThreshold == "Y")
                {
                    PayCalculatorWithThreshold withTaxThreshold = new PayCalculatorWithThreshold();
                    taxRate = PayCalculatorWithThreshold.CalculateTax(grossPay); //Should return us 2 values TaxRateA and TaxRateB
                }
                else if (paySlip.HasTaxThreshold == "N")
                {
                    PayCalculatorNoThreshold noTaxThreshold = new PayCalculatorNoThreshold();
                    taxRate = PayCalculatorNoThreshold.CalculateTax(grossPay);
                }
                //Calculate taxAmount
                taxAmount = Math.Round((taxRate[0] * grossPay) - taxRate[1], 2);

                //Calculate super 
                superAmount = PayCalculator.CalculateSuper(grossPay, superRate);

                //Calculate  netPay
                netPay = PayCalculator.CalculateNetPay(grossPay, superAmount, taxAmount);
                //institate object to store a row of all calculation
                var paysliprow = new PaySlip
                {
                    Id = paySlip.Id,
                    FirstName = paySlip.FirstName,
                    LastName = paySlip.LastName,
                    HourlyRate = paySlip.HourlyRate,
                    HasTaxThreshold = paySlip.HasTaxThreshold,
                    GrossPay = grossPay,
                    TaxAmount = taxAmount,
                    SuperAmount = superAmount,
                    NetPay = netPay,
                    TypeEmployee = paySlip.TypeEmployee
                };
                list.Add(paysliprow);
            }
            //databinding List<PaySlip> to newDataGrid
            newDataGrid.DataContext = list;
        }


        /// <summary> 
        /// Saves payment summary detail of the team to a new csv file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_click_save(object sender, RoutedEventArgs e)
        {
            //var savePaySlip = new List<PaySlip>();
            //savePaySlip = list;
            var saveFileName = @"C:\Users\AKATY\source\repos\OOP_PayCalculator\saved_csv_file\outData\new_team_Payslip-" + DateTime.Now.ToFileTime() + ".csv";
            using (var writer = new StreamWriter(saveFileName))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(list);
                }
            }
            MessageBox.Show("New PaySlip csv file saved!");
        }



        private void empDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //crash if deleted
        }

        private void newDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //crash if deleted
        }
    }
}
