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
}