using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class AddAppointmentForm : Form
    {
        private TimeZoneInfo userTimeZone;
        private int currentUserId;


        public AddAppointmentForm(int currentUserId)
        {
            InitializeComponent();
            userTimeZone = TimeZoneInfo.Local; // Detect user's timezone
            this.currentUserId = currentUserId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

                DateTime selectedDate = dtpDate.Value.Date; // Get selected date (ignoring time)

                // Convert selected time strings to DateTime objects
                string startTimeStr = comboBoxStartTime.SelectedItem.ToString();
                string endTimeStr = comboBoxEndTime.SelectedItem.ToString();

                DateTime parsedStartTime = DateTime.ParseExact(startTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);
                DateTime parsedEndTime = DateTime.ParseExact(endTimeStr, "hh:mm tt", CultureInfo.InvariantCulture);

                // Merge date and time
                DateTime startLocal = selectedDate.AddHours(parsedStartTime.Hour).AddMinutes(parsedStartTime.Minute);
                DateTime endLocal = selectedDate.AddHours(parsedEndTime.Hour).AddMinutes(parsedEndTime.Minute);

                if (endLocal <= startLocal)
                {
                    MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Convert local time to UTC before saving
                DateTime startUtc = TimeZoneInfo.ConvertTimeToUtc(startLocal, userTimeZone);
                DateTime endUtc = TimeZoneInfo.ConvertTimeToUtc(endLocal, userTimeZone);

                // Create appointment object
                Appointment newAppointment = new Appointment
                {
                    CustomerId = customerId,
                    Title = title,
                    Description = description,
                    Location = location,
                    Contact = contact,
                    Type = type,
                    Url = url,
                    Start = startUtc,
                    End = endUtc,
                    UserId = currentUserId,
                    CreatedBy = "test"
                };

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

        // ✅ Modified: Ensures time selection aligns with the user's local timezone
        private void PopulateTimeComboBox(ComboBox combo)
        {
            combo.Items.Clear();

            // Define Eastern Timezone
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            // Create business hours in Eastern Time (in UTC)
            DateTime estStartUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 9, 0, 0), estZone);
            DateTime estEndUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 17, 0, 0), estZone);

            // Convert these UTC times to the user's local time zone
            DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(estStartUtc, TimeZoneInfo.Local);
            DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(estEndUtc, TimeZoneInfo.Local);

            // Populate dropdown with 15-minute increments
            DateTime time = localStart;
            while (time <= localEnd)
            {
                combo.Items.Add(time.ToString("hh:mm tt")); // Display in local time
                time = time.AddMinutes(15); // Increment by 15 minutes
            }

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }



        private void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            // Populate customer list
            DataTable dtCustomers = CustomerDAO.GetCustomers();
            comboBoxCustomer.DisplayMember = "customerName";
            comboBoxCustomer.ValueMember = "customerId";
            comboBoxCustomer.DataSource = dtCustomers;

            // Populate time dropdowns
            PopulateTimeComboBox(comboBoxStartTime);
            PopulateTimeComboBox(comboBoxEndTime);
        }
    }
}
