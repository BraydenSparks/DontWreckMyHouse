using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepository : IGuestRepository
    {
        private const string HEADER = "guest_id,first_name,last_name,email,phone,state";
        private readonly string filePath;

        public List<Guest> FindAllGuests()
        {
            throw new NotImplementedException();
        }

        public Guest FindGuestById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
