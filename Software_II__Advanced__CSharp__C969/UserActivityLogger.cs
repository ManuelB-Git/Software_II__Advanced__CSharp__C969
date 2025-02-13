using System;
using System.IO;

namespace Software_II__Advanced__CSharp__C969
{
    // This class is responsible for logging user activity, specifically login events.
    public static class UserActivityLogger
    {
        private static readonly string logFilePath = "Login_History.txt";

        public static void LogLogin(string username, DateTime loginTime)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.Local;
            DateTime userLocalTime = TimeZoneInfo.ConvertTime(loginTime, userTimeZone);

            TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTime(loginTime, easternTimeZone);

            string logEntry = $"User: {username} logged in (Eastern Time: {easternTime:yyyy-MM-dd HH:mm:ss} {easternTimeZone.StandardName}){Environment.NewLine}";

            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
