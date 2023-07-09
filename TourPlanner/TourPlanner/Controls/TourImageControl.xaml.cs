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
using TourPlanner.Viewmodels;
using static iText.Layout.Borders.Border;

namespace TourPlanner.Controls
{
    /// <summary>
    /// Interaction logic for TourImageControl.xaml
    /// </summary>
    public partial class TourImageControl : UserControl
    {
        public TourImageControl()
        {
            InitializeComponent();

            
        }

        private void TourImageControl_Loaded(object sender, RoutedEventArgs e)
        {
            var currentViewModel = DataContext as ManageToursViewModel;
            currentViewModel.CurrentSelectedTourUpdated += ManageTourViewModel_CurrentTourUpdated;
        }

        private void ManageTourViewModel_CurrentTourUpdated(object sender, EventArgs e)
        {
            string test = "teyxt";
            ManageToursViewModel viewModel = sender as ManageToursViewModel;
            string imagePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images\\" + viewModel.CurrentSelectedTour.ImageId + ".jpg";
            BitmapImage bitmapImage = new BitmapImage();

            try
            {
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();
                ImagePresenter.Source = bitmapImage;
            }
            catch(Exception ex)
            {
                //No image path
                ImagePresenter.Source = null;
            }
            
            
        }
    }
}
