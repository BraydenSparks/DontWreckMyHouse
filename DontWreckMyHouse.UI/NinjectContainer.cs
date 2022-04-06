using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class NinjectContainer
    {
        public static StandardKernel Kernel { get; set; }

        public static void Configure()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            Kernel.Bind<View>().To<View>();
        }
    }
}
