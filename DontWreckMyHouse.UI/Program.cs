using Ninject;
using System;

namespace DontWreckMyHouse.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.Kernel.Get<Controller>();
            controller.Run();
        }
    }
}
