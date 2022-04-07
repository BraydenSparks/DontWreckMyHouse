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

        public Result<Host> FindById(string id)
        {
            var all = FindAllHosts();
            Result<Host> result = new Result<Host>();
            if (all.Success)
            {
                result.Value = all.Value.FirstOrDefault(h => h.Id == id);
            }
            if(result.Value == null)
            {
                result.AddMessage($"No Host found with id {id}");
            }
            return result;
        }

        public Result<List<Host>> FindByLastName(string name)
        {
            var normalizedName = name[0].ToString().ToUpper() + name.Substring(1);


            var all = FindAllHosts();
            Result<List<Host>> result = new Result<List<Host>>();
            if (all.Success)
            {
                result.Value = all.Value.Where(h => h.LastName.StartsWith(normalizedName)).OrderBy(h => h.LastName).ToList();
            }
            if (result.Value.Count < 1)
            {
                result.AddMessage($"No Hosts starting with {name}");
            }
            return result;
        }
    }
}
