using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class HostFileRepository : IHostRepository
    {
        private const string HEADER = "";
        private readonly string filePath;

        public List<Host> FindAllHosts()
        {
            throw new NotImplementedException();
        }

        public Host FindHostById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
