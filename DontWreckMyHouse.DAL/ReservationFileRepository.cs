using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;

        public bool Add(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public bool Cancel(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> FindByHostId()
        {
            throw new NotImplementedException();
        }

        public bool Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
