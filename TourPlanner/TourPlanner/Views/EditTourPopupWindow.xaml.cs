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
    /// Interaction logic for EditTourPopupWindow.xaml
    /// </summary>
    public partial class EditTourPopupWindow : Window
    {
        //private string OldName { get; set; }
        //private string OldFrom { get; set; }
        //private string OldTo { get; set; }
        //private string OldTransportType { get; set; }
        private int OldTourId { get; set; }

        private ManageToursViewModel viewModel;
        public EditTourPopupWindow(ManageToursViewModel vm, int oldTourId, string Name, string From, string To, string TransportType)
        {
            
            InitializeComponent();
            OldTourId = oldTourId;
            OldName.Text = Name;
            OldFrom.Text = From;
            OldTo.Text = To;
            OldTransportType.Text = TransportType;
            viewModel = vm;
            DataContext = viewModel;
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            viewModel.EditTourToDatabase(OldTourId, NewName.Text, NewFrom.Text, NewTo.Text, NewTransportType.Text);

            Close();
        }
    }
}
