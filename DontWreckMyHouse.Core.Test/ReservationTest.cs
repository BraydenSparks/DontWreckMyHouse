using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.DAL;
using NUnit.Framework;
using System;

namespace DontWreckMyHouse.Core.Test
{
    public class ReservationTest
    {
        private Guest testGuest;
        private Host testHost;

        [SetUp]
        public void Setup()
        {
            testGuest = new Guest()
            {
                Id = 1,
                FirstName = "Sullivan",
                LastName = "Lomas",
                Email = "slomas0@mediafire.com",
                PhoneNumber = "(702) 7768761",
                State = State.NV
            };

            Address hostAddress = new Address()
            {
                Street = "3 Nova Trail",
                City = "Amarillo",
                State = State.TX,
                PostalCode = 79182
            };

            testHost = new Host()
            {
                Id = "3edda6bc-ab95-49a8-8962-d50b53f84b15",
                LastName = "Yearnes",
                Email = "eyearnes0@sfgate.com",
                PhoneNumber = "(806) 1783815",
                Address = hostAddress,
                WeekdayRate = 340m,
                WeekendRate = 425m
            };
        }

        [Test]
        public void ComputeReservationTotalTest()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = testGuest;
            reservation.Host = testHost;
            reservation.StartDate = new DateTime(2022, 03, 24);
            reservation.EndDate = new DateTime(2022, 04, 05);

            reservation.Total = reservation.ComputeReservationTotal();
            decimal expected = (testHost.WeekdayRate * 8) + (testHost.WeekendRate * 4);

            Assert.AreEqual(expected, reservation.Total);     //12 days, 4 weekend, 8 weekday
        }
    }
}