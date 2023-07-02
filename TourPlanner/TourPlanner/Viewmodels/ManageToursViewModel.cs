using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Views;

namespace TourPlanner.Viewmodels
{
    public class ManageToursViewModel : ViewModelBase
    {

        public ManageToursViewModel()
        {
            //CurrentTours = new List<string>
            //{
            //    "test1",
            //    "test2",
            //    "Test3"
            //};
            GetAllToursFromDatabase();
            AddTourCommand = new Commands.AddTourCommand(this);
        }

        public List<Tour> Tours { get; set; } = new();

        public void GetAllToursFromDatabase()
        {
            Tours = dbContext.Tours.ToList();
        }

        public void AddTourToDatabase(string test)
        {
            CreateTourPopupWindow popup = new CreateTourPopupWindow();
            popup.ShowDialog();

            //string testOneValue = test;
            //CurrentTours.Add(testOneValue);
            string testTwo = "a";
        }

        private List<string> _currentTours;

        public List<string> CurrentTours
        {
            get 
            { 
                return _currentTours;
            }
            set 
            {
                _currentTours = value;
                OnPropertyChanged(nameof(CurrentTours));
            }
        }

        public ICommand AddTourCommand { get; }

        public ICommand DeleteTourCommand { get; }

        public ICommand ModifyTourCommand { get; }
    }
}
