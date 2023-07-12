using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.Viewmodels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for CreateTourLogPopupWindow.xaml
    /// </summary>
    public partial class CreateTourLogPopupWindow : Window
    {
        private ManageToursViewModel viewModel;
        public CreateTourLogPopupWindow(ManageToursViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            DataContext = viewModel;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            //viewModel.AddTourToDatabase(NameTextBox.Text, FromTextBox.Text, ToTextBox.Text, TransportTypeTextBox.Text);
            viewModel.TourPlannerLogicManager.AddTourLogToDatabase(CommentTextBox.Text, DateAndTimeTextBox.Text, DifficultyTextBox.Text, TotalTimeTextBox.Text, RatingTextBox.Text, viewModel.CurrentSelectedTour.Id, viewModel.logsOfCurrentTour);

            Close();
        }
    }
}
