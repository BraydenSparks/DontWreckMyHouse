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

            var guest = GetGuest();
            if (guest.Success)
            {
                view.DisplaySuccess(guest.Value.ToString());
                var host = GetHost();
                if (host.Success)
                {
                    view.DisplaySuccess(host.Value.ToString());
                    host = ReportFutureReservations(host.Value);
                }
                if (host.Success)
                {
                    var start = view.PromptDateTime("Start:", DateTime.Today);
                    var end = view.PromptDateTime("End:",start);
                    // Validate not taken
                    if (CheckAvailability(start, end, host.Value))
                    {
                        Reservation reservation = new Reservation()
                        {
                            Host = host.Value,
                            Guest = guest.Value,
                            StartDate = start,
                            EndDate = end
                        };
                        reservation.Total = reservation.ComputeReservationTotal();
                        if(view.PromptYesNo($"Is total:{reservation.Total:c} okay?"))
                        {
                            var result = reservationService.Add(reservation);
                            if (result.Success)
                            {
                                view.Display("Reservation was placed.");
                            }
                            else
                            {
                                view.DiplayErrorMessages(result.Messages);
                            }
                        }
                        else
                        {
                            view.DisplayError("Reservation Canceled by user.");
                        }
                    }
                    else
                    {
                        view.DisplayError("Error: Was not Available!");
                    }
                }
                view.DiplayErrorMessages(host.Messages);
            }
            view.DiplayErrorMessages(guest.Messages);
        }

        private bool CheckAvailability(DateTime start, DateTime end, Host host)
        {
            var all = reservationService.FindByHostId(host.Id);
            if (all.Success)
            {
                all.Value = all.Value.Where(r => r.StartDate <= end && r.EndDate >= start).ToList();
                if(all.Value.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void ViewReservation()
        {
            view.DisplayHeader(MainMenuOption.ViewReservation.ToLabel());
            var hostResult = GetHost();
            if (hostResult.Success)
            {
                hostResult = ReportAllReservations(hostResult.Value);
            }
            view.DiplayErrorMessages(hostResult.Messages);        
        }

        private Result<Host> GetHost()
        {
            var option = view.SelectSearchOption("Search Host By:");
            Result<Host> hostResult = new Result<Host>();
            switch (option)
            {
                case SearchOption.ID:
                    var id = view.PromptString("Enter Host Id as string: ");
                    hostResult = hostService.FindById(id);
                    break;
                case SearchOption.LastName:
                    var name = view.PromptString("Host last name starts with: ");
                    var possibleHostResults = hostService.FindByLastName(name);
                    if (possibleHostResults.Success)
                    {
                        hostResult.Value = view.PromptSelect(possibleHostResults.Value, "Select a Host: ");
                    }
                    else
                    {
                        foreach(string message in possibleHostResults.Messages)
                        {
                            hostResult.AddMessage(message);
                        }
                    }
                    break;
                case SearchOption.Exit:
                    hostResult.AddMessage("");
                    break;
            }
            return hostResult;
        }

        private Result<Guest> GetGuest()
        {
            var option = view.SelectSearchOption("Search Guest By:");
            Result<Guest> guestResult = new Result<Guest>();
            switch (option)
            {
                case SearchOption.ID:
                    var id = view.PromptInt("Enter Guest Id as int: ");
                    guestResult = guestService.FindGuestById(id);
                    break;
                case SearchOption.LastName:
                    var name = view.PromptString("Guest last name starts with: ");
                    var possibleGuestResults = guestService.FindByLastName(name);
                    if (possibleGuestResults.Success)
                    {
                        guestResult.Value = view.PromptSelect(possibleGuestResults.Value, "Select a Guest: ");
                    }
                    else
                    {
                        foreach (string message in possibleGuestResults.Messages)
                        {
                            guestResult.AddMessage(message);
                        }
                    }
                    break;
                case SearchOption.Exit:
                    guestResult.AddMessage("");
                    break;
            }
            return guestResult;
        }

        private Result<Host> ReportAllReservations(Host host)
        {
            var reservationResult = reservationService.FindByHostId(host.Id);
            if (reservationResult.Success)
            {
                view.DisplaySuccess($"{reservationResult.Value.Count} Results for: {host.Address}");
                foreach (Reservation reservation in reservationResult.Value)
                {
                    reservation.Guest = guestService.FindGuestById(reservation.Guest.Id).Value;
                    view.Display(reservation.ToString());
                }
            }
            var result = new Result<Host>() { Value=host};
            foreach (string message in reservationResult.Messages)
            {
                result.AddMessage(message);
            }
            return result;
        }

        private Result<Host> ReportFutureReservations(Host host)
        {
            var reservationResult = reservationService.FindByHostId(host.Id);
            reservationResult.Value = reservationResult.Value.Where(r => r.StartDate > DateTime.Today).ToList();
            if (reservationResult.Success)
            {
                view.DisplaySuccess($"{reservationResult.Value.Count} Results for: {host.Address}");
                foreach (Reservation reservation in reservationResult.Value)
                {
                    reservation.Guest = guestService.FindGuestById(reservation.Guest.Id).Value;
                    view.Display(reservation.ToString());
                }
            }
            var result = new Result<Host>() { Value = host };
            foreach (string message in reservationResult.Messages)
            {
                result.AddMessage(message);
            }
            return result;
        }




    }
}
