using System;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = AppointmentDAO.GetAppointments();
            dataGridView2.DataSource = CustomerDAO.GetCustomers();
        }

        

        private void MainForm_Load(object sender, EventArgs e)
        {

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
            // Open the AddAppointmentForm as a modal dialog.
            using (AddAppointmentForm addForm = new AddAppointmentForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the DataGridView after a successful addition.
                    dataGridView1.DataSource = AppointmentDAO.GetAppointments();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Ensure that a row is selected.
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.");
                return;
            }

            try
            {
                // Build an Appointment object from the selected row.
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
                    // Assuming stored values are in UTC.
                    Start = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["start"].Value),
                    End = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["end"].Value)
                };

                // Open the UpdateAppointmentForm, passing the appointment to update.
                using (UpdateAppointmentForm updateForm = new UpdateAppointmentForm(appointment))
                {
                    if (updateForm.ShowDialog() == DialogResult.OK)
                    {
                        // Refresh the DataGridView after a successful update.
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
                    // Refresh the DataGridView after a successful add.
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
                    // Optionally, you can also retrieve and store CityName.
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
    }
}
