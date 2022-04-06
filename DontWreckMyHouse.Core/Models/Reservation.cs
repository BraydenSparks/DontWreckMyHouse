using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Host Host { get; set; }
        public Guest Guest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }

        public decimal ComputeReservationTotal()
        {
            DateTime start = StartDate;
            TimeSpan span = EndDate - start;
            var days = span.Days;

            int weekdays = 0;
            int weekends = 0;
            while (start < EndDate)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++weekdays;
                }
                start = start.AddDays(1);
            }
            weekends = days - weekdays;
            return weekdays * Host.WeekdayRate + weekends * Host.WeekendRate;
        }
    }
}
