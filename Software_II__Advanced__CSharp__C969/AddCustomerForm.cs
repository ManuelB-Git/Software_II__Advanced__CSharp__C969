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
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Customer newCustomer = new Customer
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    CityId = Convert.ToInt32(comboBoxCity.SelectedValue),
                    PostalCode = txtPostalCode.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };

                CustomerManager.AddCustomer(newCustomer);
                MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            DataTable dtCities = CityDAO.GetCities();
            comboBoxCity.DisplayMember = "city";      
            comboBoxCity.ValueMember = "cityId";        
            comboBoxCity.DataSource = dtCities;
        }
    }
}
