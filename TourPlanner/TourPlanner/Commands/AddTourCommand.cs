using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    public class AddTourCommand : CommandBase
    {
        private ManageToursViewModel _manageToursViewModel;

        public AddTourCommand(ManageToursViewModel manageToursViewModel)
        {
            _manageToursViewModel = manageToursViewModel;
            
        }

        public override void Execute(object? parameter)
        {
            //_manageToursViewModel.CurrentTours.Add("ExampleTour");
            _manageToursViewModel.OpenCreateTourPopup();
        }
    }
}
