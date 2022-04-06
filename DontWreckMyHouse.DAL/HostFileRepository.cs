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
    public class HostFileRepository : IHostRepository
    {
        private const string HEADER = "id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate";
        private readonly string filePath;
        private readonly ILogger logger;

        public HostFileRepository(string filePath, ILogger logger)
        {
            this.filePath = filePath;
            this.logger = logger;
        }

        public List<Host> FindAllHosts()
        {
            List<Host> hosts = new List<Host>();
            if (!File.Exists(filePath))
            {
                return hosts;
            }

            string[] lines = null;

            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                logger.Log(ex.Message);
                return hosts;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",");
                Host host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }

        public Host FindHostById(string id)
        {
            return FindAllHosts().FirstOrDefault(h => h.Id == id);
        }

        private Host Deserialize(string[] fields)
        {
            Address address = new Address()
            {
                Street = fields[4],
                City = fields[5],
                State = Enum.Parse<State>(fields[6],true),
                PostalCode = int.Parse(fields[7])
            };

            Host host = new Host()
            {
                Id = fields[0],
                LastName = fields[1],
                Email = fields[2],
                PhoneNumber = fields[3],
                Address = address,
                WeekdayRate = decimal.Parse(fields[8]),
                WeekendRate = decimal.Parse(fields[9])
            };

            return host;
        }
    }
}
