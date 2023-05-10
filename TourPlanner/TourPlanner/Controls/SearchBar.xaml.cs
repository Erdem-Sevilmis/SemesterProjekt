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
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
        }

        public String CurrentSearchText
        {
            get { return (String)GetValue(CurrentSearchTextProperty); }
            set { SetValue(CurrentSearchTextProperty, value); }
        }

        public static readonly DependencyProperty CurrentSearchTextProperty = DependencyProperty.Register(
                  "CurrentSearchText",
                  typeof(String),
                  typeof(UserControl),
                  new PropertyMetadata("")
              );
    }
}
