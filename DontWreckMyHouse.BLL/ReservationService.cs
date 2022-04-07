using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservationRepository reservationRepo;
        private readonly IHostRepository hostRepo;
        private readonly IGuestRepository guestRepo;

        public ReservationService(IReservationRepository reservationRepo, IHostRepository hostRepo, IGuestRepository guestRepo)
        {
            this.reservationRepo = reservationRepo;
            this.hostRepo = hostRepo;
            this.guestRepo = guestRepo;
        }

        public Result<List<Reservation>> FindByHostId(string hostId)
        {
            var result = new Result<List<Reservation>> { Value = reservationRepo.FindByHostId(hostId) };
            
            // foreach result get guest by guest id and get host by host id


            return result;
        }
        public Result<Reservation> Add(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>() { Value = reservation };
            if(!reservationRepo.Add(reservation))
            {
                result.AddMessage("Error: Reservation was not Added.");
            }
            return result;
        }
        public Result<Reservation> Update(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>() { Value = reservation };
            if (!reservationRepo.Update(reservation))
            {
                result.AddMessage("Error: Reservation was not Updated.");
            }
            return result;
        }
        public Result<Reservation> Cancel(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>() { Value = reservation };
            if (!reservationRepo.Cancel(reservation))
            {
                result.AddMessage("Error: Reservation was not Canceled.");
            }
            return result;
        }
    }
}