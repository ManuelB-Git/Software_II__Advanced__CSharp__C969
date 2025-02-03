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

                Appointment newAppointment = new Appointment
                {
                    CustomerId = customerId,
                    Title = title,
                    Description = description,
                    Location = location,
                    Contact = contact,
                    Type = type,
                    Url = url,
                    Start = startDateTime.ToUniversalTime(),
                    End = endDateTime.ToUniversalTime(),
                    UserId = 1,          
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

        private void PopulateTimeComboBox(ComboBox combo)
        {
            combo.Items.Clear();
            DateTime time = DateTime.Today.AddHours(8);  
            DateTime endTime = DateTime.Today.AddHours(22); 
            while (time <= endTime)
            {
                combo.Items.Add(time.ToString("hh:mm tt")); 
                time = time.AddMinutes(15);
            }
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }


        private void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            DataTable dtCustomers = CustomerDAO.GetCustomers(); 
            comboBoxCustomer.DisplayMember = "customerName";
            comboBoxCustomer.ValueMember = "customerId";
            comboBoxCustomer.DataSource = dtCustomers;

            PopulateTimeComboBox(comboBoxStartTime);
            PopulateTimeComboBox(comboBoxEndTime);
        }
    }
}
