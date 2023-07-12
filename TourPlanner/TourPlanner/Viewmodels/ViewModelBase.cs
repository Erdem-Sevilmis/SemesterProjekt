using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Viewmodels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public DAL.TourPlannerContext dbContext;
        public DAL.DataManager TourPlannerDataManager;
        public BL.TourManager TourPlannerLogicManager;

        public ViewModelBase()
        {
            //dbContext = new DAL.TourPlannerContext();
            TourPlannerDataManager = new DAL.DataManager();
            TourPlannerLogicManager = new BL.TourManager(TourPlannerDataManager);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
