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
using TourPlanner.Commands;

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
            // You can also validate the data going into the DataContext using the event args
            string someTest = "";
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    AddTourCommand.Execute(null);
        //}
    }
}
