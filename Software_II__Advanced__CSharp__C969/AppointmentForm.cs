using System;
using System.Management;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class AppointmentForm : Form
    {
        public AppointmentForm()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            dataGridViewAppointments.DataSource = AppointmentDAO.GetAppointments();
            // Optionally, convert UTC times to local time before binding.
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve values from your input controls.
                int customerId = int.Parse(txtCustomerId.Text.Trim());
                int userId = 1; // Use the logged-in user's id.
               // string title = txtTitle.Text.Trim();
               // string description = txtDescription.Text.Trim();
               // string location = txtLocation.Text.Trim();
               // string contact = txtContact.Text.Trim();
               // string type = txtType.Text.Trim();
               // string url = txtUrl.Text.Trim();
               // DateTime start = dateTimePickerStart.Value.ToUniversalTime(); // Store in UTC
               // DateTime end = dateTimePickerEnd.Value.ToUniversalTime();
                DateTime now = DateTime.UtcNow;
                string createdBy = "test"; // or the logged-in user

                //AppointmentDAO.AddAppointment(customerId, userId, title, description, location, contact, type, url, start, end, now, createdBy);
                LoadAppointments();
            }
            catch (BusinessHoursException bhe)
            {
                MessageBox.Show(bhe.Message, "Business Hours Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverlappingAppointmentException oae)
            {
                MessageBox.Show(oae.Message, "Overlapping Appointment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Similar event handlers for Update and Delete.
    }
}
