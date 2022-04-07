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
            // ID: 8, 08/12/2020 - 08/18/2020, Guest: Carncross, Tremain, Email: tcarncross2@japanpost.jp
            return string.Format("ID:{0} {1:d} - {2:d}, Guest: {3}",Id,StartDate,EndDate,Guest);
        }

        public decimal ComputeReservationTotal()
        {
            if(StartDate >= EndDate)
            {
                return 0;
            }

            DateTime start = StartDate;
            TimeSpan span = EndDate - start;
            var days = span.Days;

            int weekdays = 0;
            int weekends = 0;
            while (start < EndDate)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekdays++;
                }
                start = start.AddDays(1);
            }
            weekends = days - weekdays;
            return weekdays * Host.WeekdayRate + weekends * Host.WeekendRate;
        }
    }
}
