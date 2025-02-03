using System;
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
            PopulateFields();
        }

        /// <summary>
        /// Prepopulates the form controls with the appointment's current data.
        /// </summary>
        private void PopulateFields()
        {
            txtCustomerId.Text = currentAppointment.CustomerId.ToString();
            txtTitle.Text = currentAppointment.Title;
            txtDescription.Text = currentAppointment.Description;
            txtLocation.Text = currentAppointment.Location;
            txtContact.Text = currentAppointment.Contact;
            txtType.Text = currentAppointment.Type;
            txtUrl.Text = currentAppointment.Url;
            // Convert the stored UTC times to local time for display.
            dtpStart.Value = currentAppointment.Start.ToLocalTime();
            dtpEnd.Value = currentAppointment.End.ToLocalTime();
        }

 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Update the appointment object with the values from the form.
                currentAppointment.CustomerId = int.Parse(txtCustomerId.Text.Trim());
                // The UserId may remain unchanged or be updated if needed.
                currentAppointment.Title = txtTitle.Text.Trim();
                currentAppointment.Description = txtDescription.Text.Trim();
                currentAppointment.Location = txtLocation.Text.Trim();
                currentAppointment.Contact = txtContact.Text.Trim();
                currentAppointment.Type = txtType.Text.Trim();
                currentAppointment.Url = txtUrl.Text.Trim();
                // Convert the local time from the DateTimePickers to UTC.
                currentAppointment.Start = dtpStart.Value.ToUniversalTime();
                currentAppointment.End = dtpEnd.Value.ToUniversalTime();
                currentAppointment.UpdatedBy = "test"; // Replace with the logged-in username.

                // Call the AppointmentManager to update the appointment.
                AppointmentManager.UpdateAppointment(currentAppointment);
                MessageBox.Show("Appointment updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indicate success and close the form.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
