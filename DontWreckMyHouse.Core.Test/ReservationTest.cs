using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;

namespace DontWreckMyHouse.Core.Test
{
    public class ReservationTest
    {
        private Guest guest;
        private Host host;

        [SetUp]
        public void Setup()
        {
            guest = new Guest();
            guest.Id = 1;
            guest.FirstName = "Sullivan";
            guest.LastName = "Lomas";
            guest.Email = "slomas0 @mediafire.com";
            guest.PhoneNumber = "(702) 7768761";
            guest.State = State.NV;

            Address hostAddress = new Address();
            hostAddress.Street = "3 Nova Trail";
            hostAddress.City = "Amarillo";
            hostAddress.State = State.TX;
            hostAddress.PostalCode = 79182;

            host = new Host();
            host.Id = "3edda6bc-ab95-49a8-8962-d50b53f84b15";
            host.LastName = "Yearnes";
            host.Email = "eyearnes0 @sfgate.com";
            host.PhoneNumber = "(806) 1783815";
            host.Address = hostAddress;
            host.WeekdayRate = 340m;
            host.WeekendRate = 425m;
        }

        [Test]
        public void ComputeReservationTotalTest()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = guest;
            reservation.Host = host;
            reservation.StartDate = new DateTime(2022, 03, 24);
            reservation.EndDate = new DateTime(2022, 04, 05);

            reservation.Total = reservation.ComputeReservationTotal();
            decimal expected = (host.WeekdayRate * 8) + (host.WeekendRate * 4);

            Assert.AreEqual(expected, reservation.Total);     //12 days, 4 weekend, 8 weekday
        }
    }
}