using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Resources;

using System.Windows.Forms;

namespace Software_II__Advanced__CSharp__C969
{
    public partial class LoginForm : Form
    {
        ResourceManager resManager = new ResourceManager("Software_II__Advanced__CSharp__C969.Strings", typeof(LoginForm).Assembly);
        private LiveClock _liveClock;


        public LoginForm()
        {
            InitializeComponent();
            labelDisplayUserLanguage.Text = CultureInfo.CurrentUICulture.DisplayName;
            lblLocation.Text = CultureInfo.CurrentCulture.DisplayName;

            _liveClock = new LiveClock(lblClock);
            _liveClock.Start();


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (ValidateUser(userName, password, out int userId))
            {
                // Log successful login
                UserActivityLogger.LogLogin(userName, DateTime.UtcNow);

                // Open main form and pass the userId
                MainForm mainForm = new MainForm(userId);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                string errorMessage = resManager.GetString("loginInvalid", CultureInfo.CurrentUICulture);
                MessageBox.Show(errorMessage, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser(string userName, string password, out int userId)
        {
            userId = -1; // Default to -1 (invalid user)
            bool isValidUser = false;

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT userId FROM user WHERE userName = @userName AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                    isValidUser = true;
                }
            }

            return isValidUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
 