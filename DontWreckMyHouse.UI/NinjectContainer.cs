using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.DAL;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
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

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationFilePath = Path.Combine(projectDirectory, "Data", "Reservations");
            string hostFilePath = Path.Combine(projectDirectory, "Data", "hosts.csv");
            string guestFilePath = Path.Combine(projectDirectory, "Data", "guests.csv");
            string logFilePath = Path.Combine(projectDirectory, "Data", "Log.txt");

            Kernel.Bind<ILogger>().To<NullLogger>().WithConstructorArgument(logFilePath);

            Kernel.Bind<IReservationRepository>().To<ReservationFileRepository>().WithConstructorArgument("directory", reservationFilePath);
            Kernel.Bind<IHostRepository>().To<HostFileRepository>().WithConstructorArgument("filePath", hostFilePath);
            Kernel.Bind<IGuestRepository>().To<GuestFileRepository>().WithConstructorArgument("filePath", guestFilePath);

            Kernel.Bind<ReservationService>().To<ReservationService>();
            Kernel.Bind<HostService>().To<HostService>();
            Kernel.Bind<GuestService>().To<GuestService>();
        }
    }
}
