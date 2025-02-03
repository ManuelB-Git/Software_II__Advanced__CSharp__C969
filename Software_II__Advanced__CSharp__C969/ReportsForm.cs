using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void btnReport1_Click(object sender, EventArgs e)
        {
            List<Appointment> appointments = AppointmentDAO.GetAppointmentsList();

            var report1 = appointments
                //Lambda expression
                // Group appointments by month and type
                .GroupBy(a => new { Month = a.Start.Month, a.Type })
                //Lambda expression
                // Project each group into a new anonymous type
                .Select(g => new
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    AppointmentType = g.Key.Type,
                    Count = g.Count()
                })
                //Lambda expression
                // Order the results by month and then by appointment type
                .OrderBy(r => DateTime.ParseExact(r.Month, "MMMM", CultureInfo.CurrentCulture))
                .ThenBy(r => r.AppointmentType)
                .ToList();

            dgvReports.DataSource = report1;
        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            List<Appointment> appointments = AppointmentDAO.GetAppointmentsList();

            var report2 = appointments
                //Lambda expression
                // Group appointments by user ID
                .GroupBy(a => a.UserId)
                // Project each group into a new anonymous type
                .Select(g => new
                {
                    Consultant = g.Key,
                    //Lambda expression
                    // Create a schedule string for each appointment
                    Schedule = string.Join("; ", g.Select(a =>
                                         a.Title + " at " +
                                         TimeZoneInfo.ConvertTimeFromUtc(a.Start, TimeZoneInfo.Local).ToString("g")))
                })
                .ToList();

            dgvReports.DataSource = report2;
        }

        private void btnReport3_Click(object sender, EventArgs e)
        {
            List<Appointment> appointments = AppointmentDAO.GetAppointmentsList();

            DataTable dtCustomers = CustomerDAO.GetCustomers();
            //Lambda expression
            // Convert DataTable to Dictionary for quick lookup
            Dictionary<int, string> customerNames = dtCustomers.AsEnumerable()
                .ToDictionary(row => row.Field<int>("customerId"), row => row.Field<string>("customerName"));

            var report3 = appointments
                //Lambda expression
                // Group appointments by customer ID
                .GroupBy(a => a.CustomerId)
                //Lambda expression
                // Project each group into a new anonymous type
                .Select(g => new
                {
                    CustomerId = g.Key,
                    //Lambda expression
                    // Lookup customer name from dictionary
                    CustomerName = customerNames.ContainsKey(g.Key) ? customerNames[g.Key] : "Unknown",
                    TotalAppointments = g.Count()
                })
                .ToList();

            dgvReports.DataSource = report3;
        }
    }
}
