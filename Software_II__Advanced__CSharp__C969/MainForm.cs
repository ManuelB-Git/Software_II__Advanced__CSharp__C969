using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class MainForm : Form
    {
        private LiveClock _liveClock;

        public int CurrentUserId { get; set; }

        private HashSet<int> remindedAppointmentIds = new HashSet<int>();

        private Timer reminderTimer;

        public MainForm(int currentUserId)
        {
            InitializeComponent();
            dataGridView1.DataSource = AppointmentDAO.GetAppointments();
            dataGridView1.CellFormatting += dataGridViewAppointments_CellFormatting;

            dataGridView2.DataSource = CustomerDAO.GetCustomers();

            _liveClock = new LiveClock(lblClock);
            _liveClock.Start();

            CurrentUserId = currentUserId;

            reminderTimer = new Timer();
            reminderTimer.Interval = 60000;
            reminderTimer.Tick += ReminderTimer_Tick;
            ReminderTimer_Tick(null, EventArgs.Empty);
            reminderTimer.Start();




        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            DateTime nowUtc = DateTime.UtcNow;
            DateTime reminderThresholdUtc = nowUtc.AddMinutes(15);

            var appointments = AppointmentDAO.GetAppointmentsListForUser(CurrentUserId);

            foreach (var appt in appointments)
            {
           
                if (appt.Start >= nowUtc && appt.Start <= reminderThresholdUtc)
                {
                    if (!remindedAppointmentIds.Contains(appt.AppointmentId))
                    {
                        DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(appt.Start, TimeZoneInfo.Local);
                        string message = $"Reminder: You have an appointment \"{appt.Title}\" scheduled at {localStart.ToString("hh:mm tt")}.";
                        MessageBox.Show(message, "Appointment Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        remindedAppointmentIds.Add(appt.AppointmentId);
                    }
                }
            }
        }



        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dataGridView1.Columns[e.ColumnIndex].Name == "start" ||
                 dataGridView1.Columns[e.ColumnIndex].Name == "end") && e.Value != null)
            {
                if (e.Value is DateTime utcDateTime)
                {
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.Local);
                    e.Value = localTime.ToString("g"); 
                    e.FormattingApplied = true;
                }
            }
        }



        private void button8_Click(object sender, EventArgs e)
        {
        
            Application.Restart();
        


        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AppointmentDAO.GetAppointments();

        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AppointmentFilter.GetAppointmentsForCurrentWeek();

        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AppointmentFilter.GetAppointmentsForCurrentMonth();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddAppointmentForm addForm = new AddAppointmentForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = AppointmentDAO.GetAppointments();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.");
                return;
            }

            try
            {
                Appointment appointment = new Appointment
                {
                    AppointmentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["appointmentId"].Value),
                    CustomerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["customerId"].Value),
                    UserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["userId"].Value),
                    Title = dataGridView1.SelectedRows[0].Cells["title"].Value.ToString(),
                    Description = dataGridView1.SelectedRows[0].Cells["description"].Value.ToString(),
                    Location = dataGridView1.SelectedRows[0].Cells["location"].Value.ToString(),
                    Contact = dataGridView1.SelectedRows[0].Cells["contact"].Value.ToString(),
                    Type = dataGridView1.SelectedRows[0].Cells["type"].Value.ToString(),
                    Url = dataGridView1.SelectedRows[0].Cells["url"].Value.ToString(),
                    Start = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["start"].Value),
                    End = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["end"].Value)
                };

                using (UpdateAppointmentForm updateForm = new UpdateAppointmentForm(appointment))
                {
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView1.DataSource = AppointmentDAO.GetAppointments();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error preparing the update: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.");
                return;
            }

            int appointmentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["appointmentId"].Value);

            if (MessageBox.Show("Are you sure you want to delete this appointment?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    AppointmentManager.DeleteAppointment(appointmentId);
                    MessageBox.Show("Appointment deleted successfully.");
                    dataGridView1.DataSource = AppointmentDAO.GetAppointments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting appointment: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (AddCustomerForm addForm = new AddCustomerForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    dataGridView2.DataSource = CustomerDAO.GetCustomers();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to update.");
                return;
            }

            try
            {
                Customer customer = new Customer
                {
                    CustomerId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["customerId"].Value),
                    CustomerName = dataGridView2.SelectedRows[0].Cells["customerName"].Value.ToString(),
                    Address = dataGridView2.SelectedRows[0].Cells["address"].Value.ToString(),
                    PostalCode = dataGridView2.SelectedRows[0].Cells["postalCode"].Value.ToString(),
                    Phone = dataGridView2.SelectedRows[0].Cells["phone"].Value.ToString(),
                    CityId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["cityId"].Value)
                };

                using (UpdateCustomerForm updateForm = new UpdateCustomerForm(customer))
                {
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView2.DataSource = CustomerDAO.GetCustomers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error preparing update: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            int customerId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["customerId"].Value);
            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    CustomerManager.DeleteCustomer(customerId);
                    MessageBox.Show("Customer deleted successfully.");
                    dataGridView2.DataSource = CustomerDAO.GetCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting customer: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (ReportsForm rptForm = new ReportsForm())
            {
                rptForm.ShowDialog();
            }
        }

        private void MonthCalendar_DateSelected_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start.Date; // Get selected date without time

            // Use the new method to fetch appointments for the selected day
            DataTable appointments = AppointmentFilter.GetAppointmentsForDay(selectedDate);

            dataGridView1.DataSource = appointments;
        }

       

    }
}
