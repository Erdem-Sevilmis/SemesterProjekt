using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TourPlanner.Viewmodels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged;

        //public DAL.TourPlannerContext dbContext;
        public DAL.DataManager TourPlannerDataManager;
        public BL.TourManager TourPlannerLogicManager;

        public ViewModelBase()
        {
            try
            {
                //dbContext = new DAL.TourPlannerContext();
                TourPlannerDataManager = new DAL.DataManager();
                TourPlannerLogicManager = new BL.TourManager(TourPlannerDataManager);
            }
            catch (Exception ex)
            {
                _log.Error("Error initializing ViewModelBase", ex);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                _log.Error("Error invoking PropertyChanged event", ex);
            }
        }
    }
}
