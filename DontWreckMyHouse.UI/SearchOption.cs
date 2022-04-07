using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public enum SearchOption
    {
        ID,
        LastName,
        Exit
    }

    public static class SearchOptionExtensions
    {
        public static string ToLabel(this SearchOption option) => option switch
        {
            SearchOption.ID => "The Id",
            SearchOption.LastName => "Last Name",
            SearchOption.Exit => "Return",
            _ => "Not an Option"
        };
    }
}
