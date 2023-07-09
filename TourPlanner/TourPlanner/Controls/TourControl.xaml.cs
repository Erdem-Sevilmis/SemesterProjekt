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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourPlanner.Models;
using TourPlanner.Viewmodels;

namespace TourPlanner.Controls
{
    /// <summary>
    /// Interaction logic for TourControl.xaml
    /// </summary>
    public partial class TourControl : UserControl
    {

        public TourControl()
        {
            InitializeComponent();
            DataContextChanged += new DependencyPropertyChangedEventHandler(UserControl1_DataContextChanged);
            string test = "asd";
        }

        void TourControlLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        void UserControl1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            string someTest = "";
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = (Tour)((Button)sender).DataContext;

            ManageToursViewModel viewModel = DataContext as ManageToursViewModel;
            if (viewModel != null)
            {
                viewModel.ExportTourToPdf(selectedItem);

            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = (Tour)((Button)sender).DataContext;

            ManageToursViewModel viewModel = DataContext as ManageToursViewModel;
            if (viewModel != null)
            {
                viewModel.OpenEditTourPopup(selectedItem);

            }
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = (Tour)((Button)sender).DataContext;

            ManageToursViewModel viewModel = DataContext as ManageToursViewModel;
            if (viewModel != null)
            {
                viewModel.DeleteTourFromDb(selectedItem);

            }

        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Tour)toursListBox.SelectedItem;
            ManageToursViewModel viewModel = DataContext as ManageToursViewModel;
            if (viewModel != null)
            {
                viewModel.ChangeCurrentSelectedTour(selectedItem);

            }
        }



        public static readonly DependencyProperty TourControlViewModelProperty = DependencyProperty.Register(
                  "TourControlView",
                  typeof(object),
                  typeof(UserControl),
                  new PropertyMetadata(null)
              );
        public object TourControlViewModel
        {
            get { return (object)GetValue(TourControlViewModelProperty); }
            set { SetValue(TourControlViewModelProperty, value); }
        }

    }
}
