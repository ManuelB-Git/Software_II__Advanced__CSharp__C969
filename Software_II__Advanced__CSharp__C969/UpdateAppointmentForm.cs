using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class UpdateAppointmentForm : Form
    {
        // The appointment to be updated.
        private Appointment currentAppointment;

        public UpdateAppointmentForm(Appointment appointment)
        {
            InitializeComponent();
            currentAppointment = appointment;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected customer.
                int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);

                // Retrieve updated details.
                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();
                string location = txtLocation.Text.Trim();
                string contact = txtContact.Text.Trim();
                string type = txtType.Text.Trim();
                string url = txtUrl.Text.Trim();

                // Get the selected date.
                DateTime selectedDate = dtpDate.Value.Date;

                // Get the selected times.
                string startTimeStr = comboBoxStartTime.SelectedItem.ToString();
                string endTimeStr = comboBoxEndTime.SelectedItem.ToString();

                DateTime parsedStartTime = DateTime.ParseExact(startTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);
                DateTime parsedEndTime = DateTime.ParseExact(endTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);

                DateTime startDateTime = selectedDate.AddHours(parsedStartTime.Hour).AddMinutes(parsedStartTime.Minute);
                DateTime endDateTime = selectedDate.AddHours(parsedEndTime.Hour).AddMinutes(parsedEndTime.Minute);

                if (endDateTime <= startDateTime)
                {
                    MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update the current appointment object.
                currentAppointment.CustomerId = customerId;
                currentAppointment.Title = title;
                currentAppointment.Description = description;
                currentAppointment.Location = location;
                currentAppointment.Contact = contact;
                currentAppointment.Type = type;
                currentAppointment.Url = url;
                currentAppointment.Start = startDateTime.ToUniversalTime();
                currentAppointment.End = endDateTime.ToUniversalTime();
                // Optionally update UpdatedBy, etc.

                AppointmentManager.UpdateAppointment(currentAppointment);
                MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PopulateTimeComboBox(ComboBox combo)
        {
            combo.Items.Clear();
            DateTime time = DateTime.Today.AddHours(8);  // 8:00 AM
            DateTime dtEnd = DateTime.Today.AddHours(22);  // 10:00 PM
            while (time <= dtEnd)
            {
                combo.Items.Add(time.ToString("hh:mm tt"));
                time = time.AddMinutes(15);
            }
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private void UpdateAppointmentForm_Load(object sender, EventArgs e)
        {
            // Populate customer ComboBox.
            DataTable dtCustomers = CustomerDAO.GetCustomers();
            comboBoxCustomer.DisplayMember = "customerName";
            comboBoxCustomer.ValueMember = "customerId";
            comboBoxCustomer.DataSource = dtCustomers;

            // Populate time ComboBoxes.
            PopulateTimeComboBox(comboBoxStartTime);
            PopulateTimeComboBox(comboBoxEndTime);

            // Preselect the customer.
            comboBoxCustomer.SelectedValue = currentAppointment.CustomerId;

            // Convert stored UTC times to local time.
            DateTime localStart = currentAppointment.Start.ToLocalTime();
            DateTime localEnd = currentAppointment.End.ToLocalTime();

            // Set the DateTimePicker to the appointment date.
            dtpDate.Value = localStart.Date;

            // Preselect the times in the ComboBoxes.
            string startTimeStr = localStart.ToString("hh:mm tt");
            string endTimeStr = localEnd.ToString("hh:mm tt");
            comboBoxStartTime.SelectedItem = startTimeStr;
            comboBoxEndTime.SelectedItem = endTimeStr;

            // Prepopulate other fields.
            txtTitle.Text = currentAppointment.Title;
            txtDescription.Text = currentAppointment.Description;
            txtLocation.Text = currentAppointment.Location;
            txtContact.Text = currentAppointment.Contact;
            txtType.Text = currentAppointment.Type;
            txtUrl.Text = currentAppointment.Url;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
