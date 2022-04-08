using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuestRepository repo;

        public GuestService(IGuestRepository repo)
        {
            this.repo = repo;
        }

        public Result<List<Guest>> FindAllGuests()
        {
            return new Result<List<Guest>> {Value = repo.FindAllGuests()};
        }

        public Result<Guest> FindGuestById(int id)
        {
            Result<Guest> result = new Result<Guest>();

            var all = FindAllGuests();
            if (all.Success)
            {
                result.Value = all.Value.FirstOrDefault(g => g.Id == id);
            }
            if(result.Value == null)
            {
                result.AddMessage("Error: Guest not Found!");
            }
            return result;
            
        }

        public Result<List<Guest>> FindByLastName(string name)
        {
            var normalizedName = name[0].ToString().ToUpper() + name.Substring(1);

            var all = FindAllGuests();
            Result<List<Guest>> result = new Result<List<Guest>>();
            if (all.Success)
            {
                result.Value = all.Value.Where(h => h.LastName.StartsWith(normalizedName)).OrderBy(h => h.LastName).ToList();
            }
            if (result.Value.Count < 1)
            {
                result.AddMessage($"No Guests starting with {name}");
            }
            return result;
        }
    }
}
