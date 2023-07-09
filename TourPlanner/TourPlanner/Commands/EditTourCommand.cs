using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    public class EditTourCommand : CommandBase
    {
        private ManageToursViewModel _manageToursViewModel;

        public EditTourCommand(ManageToursViewModel manageToursViewModel)
        {
            _manageToursViewModel = manageToursViewModel;

        }

        public override void Execute(object? parameter)
        {
            //_manageToursViewModel.OpenEditTourPopup();
        }
    }
}
