using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL.Tests
{
    public class HostFileRepositoryTest
    {
        const string TEST_PATH = @"TestData\hostsTest.csv";
        HostFileRepository repo = new HostFileRepository(TEST_PATH, new NullLogger());
        private Host testHost;

        [SetUp]
        public void Setup()
        {
            Address hostAddress = new Address();
            hostAddress.Street = "3 Nova Trail";
            hostAddress.City = "Amarillo";
            hostAddress.State = State.TX;
            hostAddress.PostalCode = 79182;

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
        public void TestGetAll_GivenValidDataFile()
        {
            List<Host> hosts = repo.FindAllHosts();

            Assert.IsNotNull(hosts);           // Check that hosts is != NULL
            Assert.AreEqual(1000, hosts.Count); // Check if count of hosts is correct

            Assert.AreEqual(testHost.Id, hosts[0].Id);
            Assert.AreEqual(testHost.LastName, hosts[0].LastName);
            Assert.AreEqual(testHost.Email, hosts[0].Email);
            Assert.AreEqual(testHost.PhoneNumber, hosts[0].PhoneNumber);
            Assert.AreEqual(testHost.WeekdayRate, hosts[0].WeekdayRate);
            Assert.AreEqual(testHost.WeekendRate, hosts[0].WeekendRate);
        }

        [Test]
        public void TestFindhostById_GivenValidID()
        {
            Host host = repo.FindHostById("3edda6bc-ab95-49a8-8962-d50b53f84b15");

            Assert.IsNotNull(host);           // Check that hosts is != NULL

            Assert.AreEqual(testHost.Id, host.Id);
            Assert.AreEqual(testHost.LastName, host.LastName);
            Assert.AreEqual(testHost.Email, host.Email);
            Assert.AreEqual(testHost.PhoneNumber, host.PhoneNumber);
            Assert.AreEqual(testHost.WeekdayRate, host.WeekdayRate);
            Assert.AreEqual(testHost.WeekendRate, host.WeekendRate);
        }

        [Test]
        public void TestFindhostById_GivenInValidID()
        {
            Host host = repo.FindHostById("Not-A-Valid-Id");

            Assert.IsNull(host);           // Check that hosts is NULL
        }

    }
}
