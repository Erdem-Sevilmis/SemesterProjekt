using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourPlanner.Viewmodels
{
    public class ManageToursViewModel : ViewModelBase
    {

        public ManageToursViewModel()
        {
            CurrentTours = new List<string>
            {
                "test1",
                "test2",
                "Test3"
            };
            AddTourCommand = new Commands.AddTourCommand(this);
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
