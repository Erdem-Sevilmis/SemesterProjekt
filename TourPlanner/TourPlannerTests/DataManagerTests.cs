using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BL;

namespace TourPlannerTests
{
    public class DataManagerTests
    {
        private TourPlannerContext _dbContext;
        private DataManager _dataManager;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TourPlannerContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new TourPlannerContext();
            _dataManager = new DataManager(_dbContext);

            SeedTestData();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        private void SeedTestData()
        {
            var tour1 = new Tour { Id = 1, Name = "Tour 1", From = "City 1", To = "City 2" };
            var tour2 = new Tour { Id = 2, Name = "Tour 2", From = "City 2", To = "City 3" };

            var tourLog1 = new TourLog { Id = 1, Comment = "Log 1", TourId = 1 };
            var tourLog2 = new TourLog { Id = 2, Comment = "Log 2", TourId = 1 };
            var tourLog3 = new TourLog { Id = 3, Comment = "Log 3", TourId = 2 };

            _dbContext.Tours.AddRange(tour1, tour2);
            _dbContext.TourLogs.AddRange(tourLog1, tourLog2, tourLog3);
            _dbContext.SaveChanges();
        }

        [Test]
        public void GetAllToursFromDatabase_ShouldReturnAllTours()
        {
            var tours = _dataManager.GetAllToursFromDatabase();

            Assert.AreEqual(2, tours.Count);
            Assert.IsTrue(tours.Any(t => t.Id == 1 && t.Name == "Tour 1"));
            Assert.IsTrue(tours.Any(t => t.Id == 2 && t.Name == "Tour 2"));
        }

        [Test]
        public void DeleteTourFromDb_WithExistingTour_ShouldRemoveTourFromDatabase()
        {
            var tourToDelete = new Tour { Id = 1, Name = "Tour 1", From = "City 1", To = "City 2" };

            _dataManager.DeleteTourFromDb(tourToDelete);

            var tour = _dbContext.Tours.FirstOrDefault(t => t.Id == tourToDelete.Id);
            Assert.IsNull(tour);
        }

        [Test]
        public void DeleteTourLogFromDb_WithExistingTourLog_ShouldRemoveTourLogFromDatabase()
        {
            var tourLogToDelete = new TourLog { Id = 1, Comment = "Log 1", TourId = 1 };

            _dataManager.DeleteTourLogFromDb(tourLogToDelete);

            var tourLog = _dbContext.TourLogs.FirstOrDefault(tl => tl.Id == tourLogToDelete.Id);
            Assert.IsNull(tourLog);
        }

        [Test]
        public void GetTourById_WithExistingTourId_ShouldReturnValidTour()
        {
            int existingTourId = 1;

            var tour = _dataManager.GetTourById(existingTourId);

            Assert.IsNotNull(tour);
            Assert.AreEqual(existingTourId, tour.Id);
            Assert.AreEqual("Tour 1", tour.Name);
        }

        [Test]
        public void GetTourByName_WithExistingTourName_ShouldReturnValidTour()
        {
            string existingTourName = "Tour 2";

            var tour = _dataManager.GetTourByName(existingTourName);

            Assert.IsNotNull(tour);
            Assert.AreEqual(existingTourName, tour.Name);
        }

        [Test]
        public void GetTourLogById_WithExistingTourLogId_ShouldReturnValidTourLog()
        {
            int existingTourLogId = 2;

            var tourLog = _dataManager.GetTourLogById(existingTourLogId);

            Assert.IsNotNull(tourLog);
            Assert.AreEqual(existingTourLogId, tourLog.Id);
            Assert.AreEqual("Log 2", tourLog.Comment);
        }

        [Test]
        public void DeleteTour_WithExistingTourId_ShouldRemoveTourFromDatabase()
        {
            int existingTourId = 1;

            _dataManager.DeleteTourById(existingTourId);

            Tour deletedTour = _dataManager.GetTourById(existingTourId);
            Assert.IsNull(deletedTour);
        }

        [Test]
        public void GetAllTourLogsByTourId_WithExistingTourId_ShouldReturnValidTourLogs()
        {
            int existingTourId = 1;

            var tourLogs = _dataManager.GetAllTourLogsByTourId(existingTourId);

            Assert.AreEqual(2, tourLogs.Count);
            Assert.IsTrue(tourLogs.Any(tl => tl.Id == 1 && tl.Comment == "Log 1"));
            Assert.IsTrue(tourLogs.Any(tl => tl.Id == 2 && tl.Comment == "Log 2"));
        }
    }
}
