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
                // Create a new Customer object from the input controls.
                Customer newCustomer = new Customer
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    AddressId = int.Parse(txtAddressId.Text.Trim()),
                    Active = chkActive.Checked
                };

                // Call the manager to add the customer.
                CustomerManager.AddCustomer(newCustomer);
                MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indicate success and close the form.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
