using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepository
    {
        public List<Reservation> FindByHostId(string hostId);
        public bool Add(Reservation reservation);
        public bool Update(Reservation reservation);    // minimum needed is HostId and ReservationId but is that know?
        public bool Cancel(Reservation reservation);    // minimum needed is HostId and ReservationId but is that know?
    }
}
