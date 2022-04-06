using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class ReservationFileRepositoryTest
    {
        private const string TEST_DIRECTORY = @"TestData\TestReservations";
        private ReservationFileRepository repo = new ReservationFileRepository(TEST_DIRECTORY, new NullLogger());
        const string TEST_FILE_PATH = @"TestData\TestReservations\3edda6bc-ab95-49a8-8962-d50b53f84b15.csv";
        const string SEED_PATH = @"TestData\seed-3edda6bc-ab95-49a8-8962-d50b53f84b15.csv";

        private Guest testGuest = new Guest()
        {
            Id = 1,
                FirstName = "Sullivan",
                LastName = "Lomas",
                Email = "slomas0@mediafire.com",
                PhoneNumber = "(702) 7768761",
                State = State.NV
            };

        private Host testHost = new Host()
        {
            Id = "3edda6bc-ab95-49a8-8962-d50b53f84b15",
                LastName = "Yearnes",
                Email = "eyearnes0@sfgate.com",
                PhoneNumber = "(806) 1783815",
                Address = new Address()
                {
                    Street = "3 Nova Trail",
                    City = "Amarillo",
                    State = State.TX,
                    PostalCode = 79182
                },
                WeekdayRate = 340m,
                WeekendRate = 425m
            };

        [SetUp]
        public void Setup()
        {
            File.Copy(SEED_PATH, TEST_FILE_PATH, true);
        }

        [Test]
        public void TestGetAll_GivenValidDataFile()
        {
            List<Reservation> reservations = repo.FindByHostId(testHost.Id);

            Assert.IsNotNull(reservations);         // Check that hosts is != NULL
            Assert.AreEqual(13,reservations.Count);   // Check if count of hosts is correct

            Assert.AreEqual(1,reservations[0].Id);  // check that host[0] is == testHostFirst
            Assert.AreEqual(new DateTime(2021,07,31), reservations[0].StartDate);
            Assert.AreEqual(new DateTime(2021, 08, 07), reservations[0].EndDate);
            Assert.AreEqual(640, reservations[0].Guest.Id);
            Assert.AreEqual(2550, reservations[0].Total);
        }

        [Test]
        public void TestAdd()
        {


            Assert.Pass();
        }

        [Test]
        public void TestUpdate()
        {


            Assert.Pass();
        }

        [Test]
        public void TestCancel()
        {


            Assert.Pass();
        }
    }
}