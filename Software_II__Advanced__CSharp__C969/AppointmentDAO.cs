using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Software_II__Advanced__CSharp__C969
{
    public static class AppointmentDAO
    {
        public static List<Appointment> GetAppointmentsList()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM appointment";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment a = new Appointment
                        {
                            AppointmentId = reader.GetInt32("appointmentId"),
                            CustomerId = reader.GetInt32("customerId"),
                            UserId = reader.GetInt32("userId"),
                            Title = reader.GetString("title"),
                            Description = reader.GetString("description"),
                            Location = reader.GetString("location"),
                            Contact = reader.GetString("contact"),
                            Type = reader.GetString("type"),
                            Url = reader.GetString("url"),
                            Start = reader.GetDateTime("start"),
                            End = reader.GetDateTime("end")
                        };
                        appointments.Add(a);
                    }
                }
            }
            return appointments;
        }
        public static DataTable GetAppointments()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM appointment";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetAppointmentsForUser(int userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM appointment WHERE userId=@userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }


        public static List<Appointment> GetAppointmentsListForUser(int userId)
        {
            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM appointment WHERE userId = @userId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment appt = new Appointment
                        {
                            AppointmentId = reader.GetInt32("appointmentId"),
                            CustomerId = reader.GetInt32("customerId"),
                            UserId = reader.GetInt32("userId"),
                            Title = reader.GetString("title"),
                            Description = reader.GetString("description"),
                            Location = reader.GetString("location"),
                            Contact = reader.GetString("contact"),
                            Type = reader.GetString("type"),
                            Url = reader.GetString("url"),
                            Start = reader.GetDateTime("start"),
                            End = reader.GetDateTime("end")
                        };
                        appointments.Add(appt);
                    }
                }
            }
            return appointments;
        }

        public static void AddAppointment(int customerId, int userId, string title, string description, string location,
                                          string contact, string type, string url, DateTime start, DateTime end,
                                          DateTime createDate, string createdBy)
        {
            
            ValidateBusinessHours(start, end);
            ValidateOverlappingAppointments(start, end, customerId);

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdateBy) " +
                               "VALUES (@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, @createDate, @createdBy, @createdBy)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                cmd.Parameters.AddWithValue("@createDate", createDate);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateAppointment(int appointmentId, int customerId, int userId, string title, string description,
                                             string location, string contact, string type, string url, DateTime start, DateTime end,
                                             DateTime updateDate, string updatedBy)
        {
            
            ValidateBusinessHours(start, end);
            ValidateOverlappingAppointments(start, end, customerId, appointmentId);

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "UPDATE appointment SET customerId=@customerId, userId=@userId, title=@title, description=@description, " +
                               "location=@location, contact=@contact, type=@type, url=@url, start=@start, end=@end, " +
                               "lastUpdate=@updateDate, lastUpdateBy=@updatedBy WHERE appointmentId=@appointmentId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);

                cmd.Parameters.AddWithValue("@updateDate", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@updatedBy", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteAppointment(int appointmentId)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM appointment WHERE appointmentId=@appointmentId";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        private static void ValidateBusinessHours(DateTime start, DateTime end)
        {
            TimeSpan businessStart = new TimeSpan(8, 0, 0);
            TimeSpan businessEnd = new TimeSpan(22, 0, 0);

            if (start.TimeOfDay < businessStart || end.TimeOfDay > businessEnd)
            {
                throw new BusinessHoursException("Appointment is outside of business hours.");
            }
        }

        
        private static void ValidateOverlappingAppointments(DateTime start, DateTime end, int customerId, int? appointmentId = null)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM appointment WHERE customerId=@customerId AND " +
                               "((start < @end AND end > @start))";
                if (appointmentId.HasValue)
                {
                    query += " AND appointmentId <> @appointmentId";
                }
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                if (appointmentId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId.Value);
                }
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    throw new OverlappingAppointmentException("This appointment overlaps with an existing appointment.");
                }
            }
        }
    }
}
