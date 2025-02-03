using System;
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
            txtAddressId.Text = currentCustomer.AddressId.ToString();
            chkActive.Checked = currentCustomer.Active;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Update the customer object with the new values.
                currentCustomer.CustomerName = txtCustomerName.Text.Trim();
                currentCustomer.AddressId = int.Parse(txtAddressId.Text.Trim());
                currentCustomer.Active = chkActive.Checked;

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
