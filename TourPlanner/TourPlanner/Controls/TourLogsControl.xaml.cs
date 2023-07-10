using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.Models;
using TourPlanner.Viewmodels;

namespace TourPlanner.Controls
{
    /// <summary>
    /// Interaction logic for TourLogsControl.xaml
    /// </summary>
    public partial class TourLogsControl : UserControl
    {

        private ManageToursViewModel CurrentViewModel { get; set; }
        public TourLogsControl()
        {
            InitializeComponent();
        }

        private void TourLogsControl_Loaded(object sender, RoutedEventArgs e)
        {
            var currentViewModel = DataContext as ManageToursViewModel;
            CurrentViewModel = currentViewModel;
            currentViewModel.CurrentSelectedTourUpdated += ManageTourViewModel_CurrentTourUpdated;
        }

        private void ManageTourViewModel_CurrentTourUpdated(object sender, EventArgs e)
        {
            string test = "teyxt";
            ManageToursViewModel viewModel = sender as ManageToursViewModel;
            

            TourLogsListView.ItemsSource = viewModel.logsOfCurrentTour;

            //string imagePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images\\" + viewModel.CurrentSelectedTour.ImageId + ".jpg";
            //BitmapImage bitmapImage = new BitmapImage();

            //try
            //{
            //    bitmapImage.BeginInit();
            //    bitmapImage.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            //    bitmapImage.EndInit();
            //    ImagePresenter.Source = bitmapImage;
            //}
            //catch (Exception ex)
            //{
            //    //No image path
            //    ImagePresenter.Source = null;
            //}


        }

        private void AddTourLogButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel.OpenCreateTourLogPopup();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //CurrentViewModel.OpenEditTourLogPopup();
            var selectedItem = (TourLog)((Button)sender).DataContext;

            CurrentViewModel.OpenEditTourLogPopup(selectedItem);
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentViewModel.OpenCreateTourLogPopup();
        }

    }
}
