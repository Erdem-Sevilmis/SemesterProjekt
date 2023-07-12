using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataManager
    {
        public readonly TourPlannerContext dbContext;

        public DataManager()
        {
            dbContext = new TourPlannerContext();
        }

        public DataManager(TourPlannerContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public List<Tour> GetAllToursFromDatabase()
        {
            return dbContext.Tours.ToList();
        }

        public void DeleteTourFromDb(Tour tourToDelete)
        {
            dbContext.Tours.Remove(tourToDelete);
            dbContext.SaveChanges();
        }

        public void DeleteTourLogFromDb(TourLog tourLogToDelete)
        {
            dbContext.TourLogs.Remove(tourLogToDelete);
            dbContext.SaveChanges();
        }


        public TourLog GetTourLogById(int tourLogId)
        {
            TourLog tourLogToEdit = dbContext.TourLogs.Where(x => x.Id == tourLogId).FirstOrDefault();
            return tourLogToEdit;

        }

        public List<TourLog> GetAllTourLogsByTourId(int tourId)
        {

            List<TourLog> tourLogs = new List<TourLog>();
            foreach (var item in dbContext.TourLogs)
            {
                if (item.TourId == tourId)
                    tourLogs.Add(item);
            }
            
            return tourLogs;

        }
        public Tour GetTourById(int tourId)
        {
            Tour tourToEdit = dbContext.Tours.Where(x => x.Id == tourId).FirstOrDefault();
            return tourToEdit;

        }

        public Tour GetTourByName(string tourName)
        {
            Tour tourToEdit = dbContext.Tours.Where(x => x.Name == tourName).FirstOrDefault();
            return tourToEdit;

        }

        public TourLog GetTourLogByName(string tourLogName)
        {
            TourLog tourLogToEdit = dbContext.TourLogs.Where(x => x.Comment == tourLogName).FirstOrDefault();
            return tourLogToEdit;

        }
        public void AddNewTourToDb(Tour tour)
        {
            dbContext.Tours.Add(tour);
            dbContext.SaveChanges();
        }
        public void DeleteTourById(int id)
        {
            Tour tour = GetTourById(id);
            dbContext.Tours.Remove(tour);
            dbContext.SaveChanges();
        }


        public void DbSaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}