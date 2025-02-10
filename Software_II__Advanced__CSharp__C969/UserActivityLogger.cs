using System;
using System.IO;

namespace Software_II__Advanced__CSharp__C969
{
    // This class is responsible for logging user activity, specifically login events.
    public static class UserActivityLogger
    {
        // Path to the log file where login history will be stored.
        private static readonly string logFilePath = "Login_History.txt";

        // Logs the login event with the username and login time.
        public static void LogLogin(string username, DateTime loginTime)
        {
            // Create a log entry string with the login time and username.
            string logEntry = $"{loginTime:yyyy-MM-dd HH:mm:ss} - User: {username} logged in{Environment.NewLine}";

            // Append the log entry to the log file.
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
