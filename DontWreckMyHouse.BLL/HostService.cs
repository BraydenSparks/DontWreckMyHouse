using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private readonly IHostRepository repo;

        public HostService(IHostRepository repo)
        {
            this.repo = repo;
        }
        public Result<List<Host>> FindAllHosts()
        {
            return new Result<List<Host>> { Value = repo.FindAllHosts() };
        }
    }
}
