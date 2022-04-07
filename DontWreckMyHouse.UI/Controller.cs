using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Models;
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

            // Get valid user input

            // compute result
            var result = reservationService.FindByHostId("");

            // handle result
            if (result.Success)
            {
                // Display reservations from result.Value
            }
            else
            {
                // Display all error messages from result.Messages
            }

        }

        private void EditReservation()
        {
            view.DisplayHeader(MainMenuOption.EditReservation.ToLabel());

            // Get valid user input

            // compute result
            var result = reservationService.FindByHostId("");

            // handle result
            if (result.Success)
            {
                // Display reservations from result.Value
            }
            else
            {
                // Display all error messages from result.Messages
            }

        }

        private void MakeReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeReservation.ToLabel());

            // Get valid user input

            // compute result
            var result = reservationService.FindByHostId("");

            // handle result
            if (result.Success)
            {
                // Display reservations from result.Value
            }
            else
            {
                // Display all error messages from result.Messages
            }

        }

        private void ViewReservation()
        {
            view.DisplayHeader(MainMenuOption.ViewReservation.ToLabel());
            var hostResult = GetHost();
            if (hostResult.Success)
            {
                var reservationResult = reservationService.FindByHostId(hostResult.Value.Id);
                if (reservationResult.Success)
                {
                    foreach(Reservation reservation in reservationResult.Value)
                    {
                        view.Display(reservation.ToString());
                    }
                }
            }
            foreach (string message in hostResult.Messages)
            {
                view.DisplayError(message);
            }

        }

        private Result<Host> GetHost()
        {
            var option = view.SelectSearchOption();
            Result<Host> hostResult = new Result<Host>();
            switch (option)
            {
                case SearchOption.ID:
                    var id = view.PromptString("Enter Host Id as string: ");
                    hostResult = hostService.FindById(id);
                    break;
                case SearchOption.LastName:
                    var name = view.PromptString("Last name starts with: ");
                    var possibleHostResults = hostService.FindByLastName(name);
                    if (possibleHostResults.Success)
                    {
                        hostResult.Value = view.PromptSelect(possibleHostResults.Value, "Select a Host: ");
                    }
                    else
                    {
                        foreach(string message in possibleHostResults.Messages)
                        {
                            hostResult.AddMessage(message
                                );
                        }
                    }
                    break;
                case SearchOption.Exit:
                    hostResult.AddMessage("User returned to menu");
                    break;
            }
            return hostResult;
        }





    }
}
