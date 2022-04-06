using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public enum MainMenuOption
    {
        ViewReservation,
        MakeReservation,
        EditReservation,
        CancelReservation,
        Exit
    }

    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option) => option switch
        {
            MainMenuOption.Exit => "Exit",
            MainMenuOption.ViewReservation => "View Reservations for Host",
            MainMenuOption.MakeReservation => "Make a Reservation",
            MainMenuOption.EditReservation => "Edit a Reservation",
            MainMenuOption.CancelReservation => "Cancel a Reservation",
            _ => "Not an Option"
        };
    }
}
