using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public State State { get; set; }

        public override string ToString()
        {
            // Fraczak, Raymond, Email: rfraczakb5@ifeng.com
            return string.Format("{0,15}, {1}, Email: {2}", LastName,FirstName,Email);
        }

    }
}
