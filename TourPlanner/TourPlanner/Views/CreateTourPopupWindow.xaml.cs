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
    /// Interaction logic for CreateTourPopupWindow.xaml
    /// </summary>
    public partial class CreateTourPopupWindow : Window
    {

        private ManageToursViewModel viewModel;
        public CreateTourPopupWindow(ManageToursViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            DataContext = viewModel;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
         
            viewModel.AddTourToDatabase(NameTextBox.Text, FromTextBox.Text, ToTextBox.Text, TransportTypeTextBox.Text);

            Close();
        }
    }
}
