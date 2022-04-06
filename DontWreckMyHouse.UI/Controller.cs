using DontWreckMyHouse.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class Controller
    {
        private readonly View view;
        private readonly ReservationService reservationService;
        private readonly HostService hostService;
        private readonly GuestService guestService;

        public Controller(View view,ReservationService reservationService,HostService hostService, GuestService guestService)
        {
            this.view = view;
            this.reservationService = reservationService;
            this.hostService = hostService;
            this.guestService = guestService;
        }

        public void Run()
        {
            view.DisplayHeader("Don't Wreck My House");
            RunAppLoop();
            view.DisplayExit("Thanks for using Don't Wreck My House");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            bool isRunning = true;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservation:
                        ViewReservation();
                        break;
                    case MainMenuOption.MakeReservation:
                        MakeReservation();
                        break;
                    case MainMenuOption.EditReservation:
                        EditReservation();
                        break;
                    case MainMenuOption.CancelReservation:
                        CancelReservation();
                        break;
                    case MainMenuOption.Exit:
                        isRunning = !Exit();
                        break;
                }
            } while (isRunning);
        }

        private bool Exit()
        {
            return view.PromptYesNo("You are about to exit. Are you sure?");
        }

        private void CancelReservation()
        {
            view.DisplayHeader(MainMenuOption.CancelReservation.ToLabel());
        }

        private void EditReservation()
        {
            view.DisplayHeader(MainMenuOption.EditReservation.ToLabel());
        }

        private void MakeReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeReservation.ToLabel());
        }

        private void ViewReservation()
        {
            view.DisplayHeader(MainMenuOption.ViewReservation.ToLabel());


        }
    }
}
