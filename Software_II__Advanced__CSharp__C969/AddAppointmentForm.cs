using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class AddAppointmentForm : Form
    {
        public AddAppointmentForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected customer.
                int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);

                // Retrieve additional appointment details.
                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();
                string location = txtLocation.Text.Trim();
                string contact = txtContact.Text.Trim();
                string type = txtType.Text.Trim();
                string url = txtUrl.Text.Trim();

                // Get the selected date.
                DateTime selectedDate = dtpDate.Value.Date; // Only the date part

                // Get the selected start and end time strings.
                string startTimeStr = comboBoxStartTime.SelectedItem.ToString();
                string endTimeStr = comboBoxEndTime.SelectedItem.ToString();

                // Parse the time strings (using invariant culture).
                DateTime parsedStartTime = DateTime.ParseExact(startTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);
                DateTime parsedEndTime = DateTime.ParseExact(endTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);

                // Combine the selected date with the chosen times.
                DateTime startDateTime = selectedDate.AddHours(parsedStartTime.Hour).AddMinutes(parsedStartTime.Minute);
                DateTime endDateTime = selectedDate.AddHours(parsedEndTime.Hour).AddMinutes(parsedEndTime.Minute);

                // Validate that end time is after start time.
                if (endDateTime <= startDateTime)
                {
                    MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create the new Appointment object.
                Appointment newAppointment = new Appointment
                {
                    CustomerId = customerId,
                    Title = title,
                    Description = description,
                    Location = location,
                    Contact = contact,
                    Type = type,
                    Url = url,
                    // Assuming appointments are stored in UTC.
                    Start = startDateTime.ToUniversalTime(),
                    End = endDateTime.ToUniversalTime(),
                    UserId = 1,          // For example, the logged-in user ID (adjust as needed)
                    CreatedBy = "test"   // Replace with the logged-in username if available
                };

                // Call the AppointmentManager to add the appointment.
                AppointmentManager.AddAppointment(newAppointment);
                MessageBox.Show("Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTimeComboBox(ComboBox combo)
        {
            combo.Items.Clear();
            // Define business hours (for example, 8:00 AM to 10:00 PM).
            DateTime time = DateTime.Today.AddHours(8);  // 8:00 AM today
            DateTime endTime = DateTime.Today.AddHours(22); // 10:00 PM today
            while (time <= endTime)
            {
                combo.Items.Add(time.ToString("hh:mm tt")); // e.g., "08:00 AM"
                time = time.AddMinutes(15);
            }
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }


        private void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            DataTable dtCustomers = CustomerDAO.GetCustomers(); // Assumes columns "customerId" and "customerName"
            comboBoxCustomer.DisplayMember = "customerName";
            comboBoxCustomer.ValueMember = "customerId";
            comboBoxCustomer.DataSource = dtCustomers;

            // Populate the time ComboBoxes.
            PopulateTimeComboBox(comboBoxStartTime);
            PopulateTimeComboBox(comboBoxEndTime);
        }
    }
}
