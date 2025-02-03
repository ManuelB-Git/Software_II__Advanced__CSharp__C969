using System;
using System.Data;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class UpdateCustomerForm : Form
    {
        private Customer currentCustomer;

        public UpdateCustomerForm(Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            PopulateFields();
        }

        private void PopulateFields()
        {
            txtCustomerName.Text = currentCustomer.CustomerName;
            txtAddress.Text = currentCustomer.Address;
            txtPostalCode.Text = currentCustomer.PostalCode;
            txtPhone.Text = currentCustomer.Phone;
        }


  

  

        private void UpdateCustomerForm_Load(object sender, EventArgs e)
        {
            DataTable dtCities = CityDAO.GetCities();
            comboBoxCity.DisplayMember = "city";
            comboBoxCity.ValueMember = "cityId";
            comboBoxCity.DataSource = dtCities;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                currentCustomer.CustomerName = txtCustomerName.Text.Trim();
                currentCustomer.Address = txtAddress.Text.Trim();
                currentCustomer.CityId = Convert.ToInt32(comboBoxCity.SelectedValue);
                currentCustomer.PostalCode = txtPostalCode.Text.Trim();
                currentCustomer.Phone = txtPhone.Text.Trim();

                CustomerManager.UpdateCustomer(currentCustomer);
                MessageBox.Show("Customer updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
