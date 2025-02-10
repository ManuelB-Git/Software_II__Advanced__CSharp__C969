using System;

namespace Software_II__Advanced__CSharp__C969
{
    // This class manages appointments by providing methods to add, update, and delete appointments.
    public static class AppointmentManager
    {
        // Adds a new appointment to the system.
        public static void AddAppointment(Appointment appt)
        {
            if (appt == null)
                throw new ArgumentNullException(nameof(appt));
            if (string.IsNullOrWhiteSpace(appt.Title))
                throw new ArgumentException("Title is required.", nameof(appt.Title));

            DateTime now = DateTime.UtcNow;
            AppointmentDAO.AddAppointment(
                appt.CustomerId,
                appt.UserId,
                appt.Title,
                appt.Description,
                appt.Location,
                appt.Contact,
                appt.Type,
                appt.Url,
                appt.Start,
                appt.End,
                now,
                appt.CreatedBy);
        }

        // Updates an existing appointment in the system.
        public static void UpdateAppointment(Appointment appt)
        {
            if (appt == null)
                throw new ArgumentNullException(nameof(appt));
            if (appt.AppointmentId <= 0)
                throw new ArgumentException("Invalid AppointmentId.", nameof(appt.AppointmentId));

            DateTime now = DateTime.UtcNow;
            AppointmentDAO.UpdateAppointment(
                appt.AppointmentId,
                appt.CustomerId,
                appt.UserId,
                appt.Title,
                appt.Description,
                appt.Location,
                appt.Contact,
                appt.Type,
                appt.Url,
                appt.Start,
                appt.End,
                now,
                appt.UpdatedBy);
        }

        // Deletes an appointment from the system.
        public static void DeleteAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
                throw new ArgumentException("Invalid AppointmentId.", nameof(appointmentId));

            AppointmentDAO.DeleteAppointment(appointmentId);
        }
    }
}
