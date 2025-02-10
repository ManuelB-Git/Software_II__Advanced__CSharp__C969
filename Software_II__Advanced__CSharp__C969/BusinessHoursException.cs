using System;

namespace Software_II__Advanced__CSharp__C969
{
    /// <summary>
    /// Exception thrown when an operation is attempted outside of business hours.
    /// </summary>
    public class BusinessHoursException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BusinessHoursException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessHoursException(string message) : base(message) { }
    }

    /// <summary>
    /// Exception thrown when an appointment overlaps with an existing appointment.
    /// </summary>
    public class OverlappingAppointmentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the OverlappingAppointmentException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OverlappingAppointmentException(string message) : base(message) { }
    }
}
