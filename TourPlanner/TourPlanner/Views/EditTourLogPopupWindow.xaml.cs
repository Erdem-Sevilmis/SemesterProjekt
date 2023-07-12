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
using DAL.Models;
using TourPlanner.Viewmodels;

namespace TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for EditTourLogPopupWindow.xaml
    /// </summary>
    public partial class EditTourLogPopupWindow : Window
    {
        private ManageToursViewModel viewModel;

        private TourLog oldTourLog;
        public EditTourLogPopupWindow(ManageToursViewModel vm, TourLog tourLogToEdit)
        {
            InitializeComponent();
            viewModel = vm;
            oldTourLog = tourLogToEdit;
            OldComment.Text = tourLogToEdit.Comment;
            DateAndTime.Text = tourLogToEdit.DateAndTime.ToString();
            Difficulty.Text = tourLogToEdit.Difficulty.ToString();
            TotalTime.Text = tourLogToEdit.TotalTime.ToString();
            Rating.Text = tourLogToEdit.Rating.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            //viewModel.EditTourToDatabase(OldTourId, NewName.Text, NewFrom.Text, NewTo.Text, NewTransportType.Text);
            viewModel.EditTourLogToDatabase(oldTourLog.Id, NewCommentTextBox.Text, NewDateTimeTextBox.Text, NewDifficultyTextBox.Text, NewTotalTimeTextBox.Text, NewRatingTextBox.Text);
            Close();
        }
    }
}
