﻿using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostRepository
    {
        public Host FindHostById(string id);

        public List<Host> FindAllHosts();
    }
}
