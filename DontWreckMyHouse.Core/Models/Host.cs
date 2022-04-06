using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Host
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
        public decimal WeekdayRate { get; set; }
        public decimal WeekendRate { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
