using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Software_II__Advanced__CSharp__C969
{
    public static class AppointmentFilter
    {
        // Retrieves appointments for the current week.
        public static DataTable GetAppointmentsForCurrentWeek()
        {
            DateTime todayUtc = DateTime.UtcNow;
            int diff = (7 + (todayUtc.DayOfWeek - DayOfWeek.Sunday)) % 7;
            DateTime weekStart = todayUtc.AddDays(-diff).Date;
            DateTime weekEnd = weekStart.AddDays(7).AddSeconds(-1);

            return GetAppointmentsInRange(weekStart, weekEnd);
        }

        // Retrieves appointments for the current month.
        public static DataTable GetAppointmentsForCurrentMonth()
        {
            DateTime todayUtc = DateTime.UtcNow;
            DateTime monthStart = new DateTime(todayUtc.Year, todayUtc.Month, 1);
            DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);

            return GetAppointmentsInRange(monthStart, monthEnd);
        }

        // ✅ NEW: Retrieves appointments for a selected day
        public static DataTable GetAppointmentsForDay(DateTime selectedDate)
        {
            // Ensure only the date is used (ignore time)
            DateTime start = selectedDate.Date;
            DateTime end = start.AddDays(1).AddSeconds(-1);

            return GetAppointmentsInRange(start, end);
        }

        // Retrieves appointments within a specified date range.
        private static DataTable GetAppointmentsInRange(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM appointment WHERE start BETWEEN @start AND @end";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }
    }
}
