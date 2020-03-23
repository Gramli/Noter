using log4net;
using Noter.ModelViews;
using Noter.Patterns;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Noter.Views
{
    /// <summary>
    /// Interaction logic for AddNoteVC.xaml
    /// </summary>
    public partial class AddNoteVC : UserControl, IClose
    {
        protected static readonly ILog logFile = LogManager.GetLogger("errorLog");
        private AddNoteVM viewModel;

        public event CancelEventHandler Close;

        public AddNoteVC(AddNoteVM viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = this.viewModel;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                this.viewModel.SaveChanges();
                this.Close?.Invoke(this, new CancelEventArgs(false));
            }
            catch (Exception ex)
            {
                AddNoteVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close?.Invoke(this, new CancelEventArgs(true));
        }
    }
}
