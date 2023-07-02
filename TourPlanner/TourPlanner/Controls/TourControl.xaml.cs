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
