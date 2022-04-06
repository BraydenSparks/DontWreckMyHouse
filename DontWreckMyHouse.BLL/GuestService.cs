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
    }
}
