using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepository
    {
        public Guest FindGuestById(int id);
        public List<Guest> FindAllGuests();
    }
}
