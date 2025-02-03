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
                // Create a new Appointment object using the data entered by the user.
                Appointment newAppointment = new Appointment
                {
                    CustomerId = int.Parse(txtCustomerId.Text.Trim()),
                    UserId = 1, // Replace with the logged-in user's ID if available.
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Location = txtLocation.Text.Trim(),
                    Contact = txtContact.Text.Trim(),
                    Type = txtType.Text.Trim(),
                    Url = txtUrl.Text.Trim(),
                    // Convert local times to UTC for storage.
                    Start = dtpStart.Value.ToUniversalTime(),
                    End = dtpEnd.Value.ToUniversalTime(),
                    CreatedBy = "test" // Replace with the logged-in username.
                };

                // Call the AppointmentManager to add the appointment.
                AppointmentManager.AddAppointment(newAppointment);
                MessageBox.Show("Appointment added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Set the DialogResult to OK to indicate success and close the form.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
