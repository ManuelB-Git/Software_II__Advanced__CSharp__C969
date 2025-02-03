using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Group by Month (from Start) and Type.
            var report1 = appointments
                .GroupBy(a => new { Month = a.Start.Month, a.Type })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    AppointmentType = g.Key.Type,
                    Count = g.Count()
                })
                .OrderBy(r => r.Month)
                .ThenBy(r => r.AppointmentType)
                .ToList();

            dgvReports.DataSource = report1;
        }

        private void btnReport2_Click(object sender, EventArgs e)
        {
            List<Appointment> appointments = AppointmentDAO.GetAppointmentsList();

            // Group by consultant (user id).  
            // In a real scenario, you might join with a Users table to get a friendly name.
            var report2 = appointments
                .GroupBy(a => a.UserId)
                .Select(g => new
                {
                    Consultant = g.Key,
                    // Create a summary string of each appointment’s title and local start time.
                    Schedule = string.Join("; ", g.Select(a => a.Title + " at " +
                                         TimeZoneInfo.ConvertTimeFromUtc(a.Start, TimeZoneInfo.Local).ToString("g")))
                })
                .ToList();

            dgvReports.DataSource = report2;
        }

        private void btnReport3_Click(object sender, EventArgs e)
        {
            List<Appointment> appointments = AppointmentDAO.GetAppointmentsList();

            var report3 = appointments
                .GroupBy(a => a.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalAppointments = g.Count()
                })
                .ToList();

            dgvReports.DataSource = report3;
        }
    }
}
