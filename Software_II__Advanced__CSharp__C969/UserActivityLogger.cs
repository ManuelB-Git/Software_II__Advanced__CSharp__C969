using System;
using System.IO;

namespace Software_II__Advanced__CSharp__C969
{
    public static class UserActivityLogger
    {
        private static readonly string logFilePath = "UserLogins.txt";

        public static void LogLogin(DateTime loginTime)
        {
            string logEntry = $"User logged in at {loginTime:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
