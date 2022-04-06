using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepository : IGuestRepository
    {
        private const string HEADER = "guest_id,first_name,last_name,email,phone,state";
        private readonly string filePath;
        private readonly ILogger logger;

        public GuestFileRepository(string filePath,ILogger logger)
        {
            this.filePath = filePath;
            this.logger = logger;
        }

        public List<Guest> FindAllGuests()
        {
            List<Guest> guests = new List<Guest>();
            if (!File.Exists(filePath))
            {
                return guests;
            }

            string[] lines = null;

            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                logger.Log(ex.Message);
                return guests;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if (guest != null)
                {
                    guests.Add(guest);
                }
            }
            return guests;
        }

        public Guest FindGuestById(int id)
        {
            return FindAllGuests().FirstOrDefault(g => g.Id == id);
        }

        private Guest Deserialize(string[] fields)
        {
            Guest guest = new Guest();
            guest.Id = int.Parse(fields[0]);
            guest.FirstName = fields[1];
            guest.LastName = fields[2];
            guest.Email = fields[3];
            guest.PhoneNumber = fields[4];
            guest.State = Enum.Parse<State>(fields[5],true);
            return guest;
        }
    }
}
