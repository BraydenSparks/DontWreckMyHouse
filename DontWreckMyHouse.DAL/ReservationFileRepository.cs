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
    public class ReservationFileRepository : IReservationRepository
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;
        private readonly ILogger logger;

        public ReservationFileRepository(string directory, ILogger logger)
        {
            this.directory = directory;
            this.logger = logger;
        }

        public bool Add(Reservation reservation)
        {
            List<Reservation> all = FindByHostId(reservation.Host.Id);
            reservation.Id = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;
            all.Add(reservation);
            WriteToFile(all, reservation.Host.Id);
            return true;
        }

        public bool Cancel(Reservation reservation)
        {
            List<Reservation> all = FindByHostId(reservation.Host.Id);
            Reservation toBeRemoved = all.FirstOrDefault(r => r.Id == reservation.Id);
            if(toBeRemoved != null)
            {
                all.Remove(toBeRemoved);
                WriteToFile(all,reservation.Host.Id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Reservation> FindByHostId(string Id)
        {
            List<Reservation> reservations = new List<Reservation>();
            string path = GetFilePath(Id);

            if (!File.Exists(path))
            {
                return reservations;
            }

            string[] lines = null;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch(IOException ex)
            {
                logger.Log(ex.Message);
                return reservations;
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",",StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields,Id);
                if(reservation != null)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public bool Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        private string GetFilePath(string hostId)
        {
            return Path.Combine(directory, $"{hostId}.csv");
        }

        private Reservation Deserialize(string[] fields, string hostId)
        {
            Reservation reservation = new Reservation()
            {
                Host = new Host() { Id = hostId},
                Id = int.Parse(fields[0]),
                StartDate = DateTime.Parse(fields[1]),
                EndDate = DateTime.Parse(fields[2]),
                Guest = new Guest() { Id = int.Parse(fields[3]) },
                Total = decimal.Parse(fields[4])
            };
            

            return reservation;
                    
        }

        private string Serialize(Reservation reservation)
        {
            return string.Format("{0},{1},{2},{3},{4}",
                    reservation.Id,
                    reservation.StartDate,
                    reservation.EndDate,
                    reservation.Guest.Id,
                    reservation.Total);
        }

        private void WriteToFile(List<Reservation> reservations, string hostId)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(hostId));
                writer.WriteLine(HEADER);

                if (reservations == null | reservations.Count == 0)
                {
                    return;
                }

                foreach (Reservation reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch(IOException ex)
            {
                logger.Log(ex.Message);
            }
        }
    }
}
