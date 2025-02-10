using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Resources;
using Newtonsoft.Json;


using System.Windows.Forms;
using System.Net;

namespace Software_II__Advanced__CSharp__C969
{
    /// <summary>
    /// Represents the login form for the application.
    /// </summary>
    public partial class LoginForm : Form
    {
        ResourceManager resManager = new ResourceManager("Software_II__Advanced__CSharp__C969.Strings", typeof(LoginForm).Assembly);
        private LiveClock _liveClock;

        /// <summary>
        /// Initializes a new instance of the LoginForm class.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            labelDisplayUserLanguage.Text = CultureInfo.CurrentUICulture.DisplayName;
            lblLocation.Text = GetUserLocation();

            _liveClock = new LiveClock(lblClock);
            _liveClock.Start();
        }

        /// <summary>
        /// Gets the user's location based on their IP address.
        /// </summary>
        /// <returns>A string representing the user's city and country.</returns>
        private string GetUserLocation()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString("http://ip-api.com/json");
                    var locationData = JsonConvert.DeserializeObject<LocationResponse>(json);

                    return $"{locationData.City}, {locationData.Country}";
                }
            }
            catch
            {
                return "Location unavailable";
            }
        }

        /// <summary>
        /// Represents the response from the location API.
        /// </summary>
        private class LocationResponse
        {
            public string Country { get; set; }
            public string City { get; set; }
        }

        /// <summary>
        /// Handles the login button click event.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (ValidateUser(userName, password, out int userId))
            {
                UserActivityLogger.LogLogin(userName, DateTime.UtcNow);

                MainForm mainForm = new MainForm(userId, this);
                mainForm.Show();
                Hide();
            }
            else
            {
                string errorMessage = resManager.GetString("loginInvalid", CultureInfo.CurrentUICulture);
                MessageBox.Show(errorMessage, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates the user's credentials against the database.
        /// </summary>
        /// <param name="userName">The username entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <param name="userId">The user ID if the credentials are valid.</param>
        /// <returns>True if the user is valid, otherwise false.</returns>
        private bool ValidateUser(string userName, string password, out int userId)
        {
            userId = -1;
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

        /// <summary>
        /// Handles the close button click event.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
 

