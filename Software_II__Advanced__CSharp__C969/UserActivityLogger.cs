using System;
using System.IO;

namespace Software_II__Advanced__CSharp__C969
{
    public static class UserActivityLogger
    {
        private static readonly string logFilePath = "Login_History.txt";

        public static void LogLogin(string username, DateTime loginTime)
        {
            string logEntry = $"{loginTime:yyyy-MM-dd HH:mm:ss} - User: {username} logged in{Environment.NewLine}";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
