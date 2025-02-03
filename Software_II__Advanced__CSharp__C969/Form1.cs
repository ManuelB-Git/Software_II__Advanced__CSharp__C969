using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class LoginForm : Form
    {
        ResourceManager resManager = new ResourceManager("Software_II__Advanced__CSharp__C969.Strings", typeof(LoginForm).Assembly);

        public LoginForm()
        {
            InitializeComponent();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (userName.Equals("test", StringComparison.OrdinalIgnoreCase) && password == "test")
            {
                UserActivityLogger.LogLogin(DateTime.Now);

                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {

                string errorMessage = resManager.GetString("loginInvalid", CultureInfo.CurrentUICulture);
                MessageBox.Show(errorMessage, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
 