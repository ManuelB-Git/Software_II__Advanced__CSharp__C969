using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class UpdateAppointmentForm : Form
    {
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
                int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);

                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();
                string location = txtLocation.Text.Trim();
                string contact = txtContact.Text.Trim();
                string type = txtType.Text.Trim();
                string url = txtUrl.Text.Trim();

                DateTime selectedDate = dtpDate.Value.Date;

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

                currentAppointment.CustomerId = customerId;
                currentAppointment.Title = title;
                currentAppointment.Description = description;
                currentAppointment.Location = location;
                currentAppointment.Contact = contact;
                currentAppointment.Type = type;
                currentAppointment.Url = url;
                currentAppointment.Start = startDateTime.ToUniversalTime();
                currentAppointment.End = endDateTime.ToUniversalTime();

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
            DateTime time = DateTime.Today.AddHours(8);  
            DateTime dtEnd = DateTime.Today.AddHours(22);  
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
            DataTable dtCustomers = CustomerDAO.GetCustomers();
            comboBoxCustomer.DisplayMember = "customerName";
            comboBoxCustomer.ValueMember = "customerId";
            comboBoxCustomer.DataSource = dtCustomers;

            PopulateTimeComboBox(comboBoxStartTime);
            PopulateTimeComboBox(comboBoxEndTime);

            comboBoxCustomer.SelectedValue = currentAppointment.CustomerId;

            DateTime localStart = currentAppointment.Start.ToLocalTime();
            DateTime localEnd = currentAppointment.End.ToLocalTime();

            dtpDate.Value = localStart.Date;

            string startTimeStr = localStart.ToString("hh:mm tt");
            string endTimeStr = localEnd.ToString("hh:mm tt");
            comboBoxStartTime.SelectedItem = startTimeStr;
            comboBoxEndTime.SelectedItem = endTimeStr;

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
