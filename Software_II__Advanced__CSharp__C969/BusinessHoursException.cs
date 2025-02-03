using System;

namespace Software_II__Advanced__CSharp__C969
{
    public class BusinessHoursException : Exception
    {
        public BusinessHoursException(string message) : base(message) { }
    }

    public class OverlappingAppointmentException : Exception
    {
        public OverlappingAppointmentException(string message) : base(message) { }
    }
}
