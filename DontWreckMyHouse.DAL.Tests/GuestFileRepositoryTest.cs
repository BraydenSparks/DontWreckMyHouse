using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL.Tests
{
    public class GuestFileRepositoryTest
    {
        const string TEST_PATH = @"TestData\guestsTest.csv";
        GuestFileRepository repo = new GuestFileRepository(TEST_PATH, new NullLogger());
        private Guest testGuest;
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

        }

        [Test]
        public void TestGetAll_GivenValidDataFile()
        {
            List<Guest> guests = repo.FindAllGuests();

            Assert.IsNotNull(guests);           // Check that guests is != NULL
            Assert.AreEqual(1000,guests.Count); // Check if count of guests is correct

            Assert.AreEqual(testGuest.Id, guests[0].Id);
            Assert.AreEqual(testGuest.FirstName, guests[0].FirstName);
            Assert.AreEqual(testGuest.LastName, guests[0].LastName);
            Assert.AreEqual(testGuest.Email,guests[0].Email);
            Assert.AreEqual(testGuest.PhoneNumber, guests[0].PhoneNumber);
            Assert.AreEqual(testGuest.State, guests[0].State);
        }

        [Test]
        public void TestFindGuestById_GivenValidID()
        {
            Guest guest = repo.FindGuestById(1);

            Assert.IsNotNull(guest);           // Check that guests is != NULL
            
            Assert.AreEqual(testGuest.Id, guest.Id);
            Assert.AreEqual(testGuest.FirstName, guest.FirstName);
            Assert.AreEqual(testGuest.LastName, guest.LastName);
            Assert.AreEqual(testGuest.Email, guest.Email);
            Assert.AreEqual(testGuest.PhoneNumber, guest.PhoneNumber);
            Assert.AreEqual(testGuest.State, guest.State);
        }

        [Test]
        public void TestFindGuestById_GivenInValidID()
        {
            Guest guest = repo.FindGuestById(1006);

            Assert.IsNull(guest);           // Check that guests is NULL
        }

    }
}
