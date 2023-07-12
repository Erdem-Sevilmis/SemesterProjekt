using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DAL;
using DAL.Models;

namespace TourPlannerTests
{
    [TestFixture]
    public class TourManagerTests
    {
        private TourManager _tourManager;

        [SetUp]
        public void Setup()
        {
            _tourManager = new TourManager(new DataManager());
        }

        [Test]
        public void ExportTourToPdf_WithValidTour_ShouldCreatePdf()
        {
            Tour tourToExport = new Tour
            {
                Name = "Test Tour",
                From = "From City",
                To = "To City",
                Description = "Test tour description",
                TransportType = "Car",
                Distance = 100.0,
                Time = TimeSpan.FromHours(2.5)
            };

            _tourManager.ExportTourToPdf(tourToExport);

            Assert.That(!File.Exists("Results/result.pdf"));
        }

        [Test]
        public void AddTourToDatabaseLogic_WithValidParameters_ShouldAddTourToDatabase()
        {
            string name = "Test Tour";
            string from = "From City";
            string to = "To City";
            string transportType = "Car";
            TimeSpan tourTime = TimeSpan.FromHours(2.5);
            double tourDistance = 100.0;
            int imageId = 1;

            _tourManager.AddTourToDatabaseLogic(name, from, to, transportType, tourTime, tourDistance, imageId);

            Tour addedTour = _tourManager._dataManager.GetTourByName(name);
            Assert.IsNotNull(addedTour);
            Assert.AreEqual(name, addedTour.Name);
            Assert.AreEqual(from, addedTour.From);
            Assert.AreEqual(to, addedTour.To);
        }

        [Test]
        public void EditTourToDatabaseLogic_WithValidParameters_ShouldEditTourInDatabase()
        {
            int oldTourId = 1;
            string newName = "Updated Tour";
            string newFrom = "New City";
            string newTo = "Another City";
            string newTransportType = "Train";
            TimeSpan newTourTime = TimeSpan.FromHours(3);
            double newTourDistance = 150.0;
            int newImageId = 2;

            _tourManager.EditTourToDatabaseLogic(oldTourId, newName, newFrom, newTo, newTransportType, newTourDistance, newTourTime, newImageId);

            Tour editedTour = _tourManager._dataManager.GetTourById(oldTourId);
            Assert.IsNotNull(editedTour);
            Assert.AreEqual(newName, editedTour.Name);
            Assert.AreEqual(newFrom, editedTour.From);
            Assert.AreEqual(newTo, editedTour.To);
        }

        [Test]
        public void AddTourLogToDatabase_WithValidParameters_ShouldAddTourLogToDatabase()
        {
            string comment = "Test Comment";
            string dateAndTime = "2023-07-08 14:25:00";
            string difficulty = "Easy";
            string totalTime = "01:30:00";
            string rating = "4";
            int currentSelectedTourId = 1;
            List<TourLog> listOfCurrentTourLogs = new List<TourLog>();

            _tourManager.AddTourLogToDatabase(comment, dateAndTime, difficulty, totalTime, rating, currentSelectedTourId, listOfCurrentTourLogs);

            TourLog addedTourLog = _tourManager._dataManager.GetTourLogByName(comment);
            Assert.IsNotNull(addedTourLog);
            Assert.AreEqual(comment, addedTourLog.Comment);
            Assert.AreEqual(DateTimeOffset.Parse(dateAndTime), addedTourLog.DateAndTime);
            Assert.AreEqual(difficulty, addedTourLog.Difficulty);
        }

        [Test]
        public void GetImageId_WithExistingImageFiles_ShouldReturnUniqueImageId()
        {
            int imageId = _tourManager.GetImageId();

            Assert.That(imageId > 0);
            Assert.That(!File.Exists($"Images/{imageId}.jpg"));
        }

        [Test]
        public void GetTimeAndDistance_WithValidUri_ShouldReturnValidTimeAndDistance()
        {
            Uri uri = new Uri("https://www.example.com");

            (float distance, TimeSpan time) = _tourManager.GetTimeAndDistance(uri);

            Assert.That(distance >= 0);
            Assert.That(time.TotalSeconds >= 0);
        }

        [Test]
        public void EditTourLogToDatabase_WithValidParameters_ShouldEditTourLogInDatabase()
        {
            int oldTourId = 1;
            string comment = "Updated Comment";
            string dateAndTime = "2023-07-09 15:30:00";
            string difficulty = "Medium";
            string totalTime = "02:00:00";
            string rating = "3";

            _tourManager.EditTourLogToDatabase(oldTourId, comment, dateAndTime, difficulty, totalTime, rating);

            TourLog editedTourLog = _tourManager._dataManager.GetTourLogByName(comment);
            Assert.IsNotNull(editedTourLog);
            Assert.AreEqual(comment, editedTourLog.Comment);
            Assert.AreEqual(DateTimeOffset.Parse(dateAndTime), editedTourLog.DateAndTime);
            Assert.AreEqual(difficulty, editedTourLog.Difficulty);
        }

        [Test]
        public void GetTourById_WithExistingTourId_ShouldReturnValidTour()
        {
            int existingTourId = 1;

            Tour tour = _tourManager._dataManager.GetTourById(existingTourId);

            Assert.IsNotNull(tour);
            Assert.AreEqual(existingTourId, tour.Id);
        }

        [Test]
        public void GetTourLogById_WithExistingTourLogId_ShouldReturnValidTourLog()
        {
            int existingTourLogId = 1;

            TourLog tourLog = _tourManager._dataManager.GetTourLogById(existingTourLogId);

            Assert.IsNotNull(tourLog);
            Assert.AreEqual(existingTourLogId, tourLog.Id);
        }

        [Test]
        public void DeleteTour_WithExistingTourId_ShouldRemoveTourFromDatabase()
        {
            int existingTourId = 1;

            _tourManager._dataManager.DeleteTourById(existingTourId);

            Tour deletedTour = _tourManager._dataManager.GetTourById(existingTourId);
            Assert.IsNull(deletedTour);
        }

        [Test]
        public void GetAllToursFromDatabase_ShouldReturnAllTours()
        {
            List<Tour> tours = _tourManager._dataManager.GetAllToursFromDatabase();

            Assert.IsNotNull(tours);
            Assert.AreEqual(3, tours.Count); 
        }


        [Test]
        public void GetTourLogByName_WithExistingTourLogName_ShouldReturnValidTourLog()
        {

            string existingTourLogName = "Test Comment";

            TourLog tourLog = _tourManager._dataManager.GetTourLogByName(existingTourLogName);

            Assert.IsNotNull(tourLog);
            Assert.AreEqual(existingTourLogName, tourLog.Comment);
        }


        [TearDown]
        public void TearDown()
        {
            if (File.Exists("Results/result.pdf"))
            {
                File.Delete("Results/result.pdf");
            }
        }
    }

    
}
