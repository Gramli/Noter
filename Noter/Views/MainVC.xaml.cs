using log4net;
using Noter.Dialogs;
using Noter.Models;
using Noter.ModelViews;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Noter.Views
{
    /// <summary>
    /// Interaction logic for MainVC.xaml
    /// </summary>
    public partial class MainVC : UserControl
    {
        protected static readonly ILog logFile = LogManager.GetLogger("errorLog");

        private MainVM mainVM;
        private DbConnector connector;

        public MainVC()
        {
            InitializeComponent();
            this.connector = new DbConnector();
            this.mainVM = new MainVM(this.connector);
            this.DataContext = this.mainVM;
            this.cmbox.SelectedIndex = 0;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddNoteVM addVM = new AddNoteVM(this.connector);
                AddNoteVC addNote = new AddNoteVC(addVM);
                ContentWindow window = new ContentWindow("Add Note", addNote);
                window.SetIcon("addIcon");
                window.SetSize(500, 500);
                window.ShowDialog();
                this.mainVM.RefreshNotes();
            }
            catch (Exception ex)
            {
                MainVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mainVM.RemoveNote(this.mainVM.SelectedNote);
                this.mainVM.RefreshNotes();
            }
            catch (Exception ex)
            {
                MainVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainVM.SelectedNote != null)
            {
                try
                {
                    AddNoteVM addVM = new AddNoteVM(this.connector, this.mainVM.SelectedNote);
                    AddNoteVC addNote = new AddNoteVC(addVM);
                    ContentWindow window = new ContentWindow("Edit Note", addNote);
                    window.SetIcon("editIcon");
                    window.SetSize(500, 500);
                    window.ShowDialog();
                    this.mainVM.RefreshNotes();
                }
                catch (Exception ex)
                {
                    MainVC.logFile.Error(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.mainVM.RefreshNotes();
            }
            catch (Exception ex)
            {
                MainVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Archive_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainVM.SelectedNote.Archived == ArchivedEnum.Archived) SetArchived(ArchivedEnum.NotArchived);
            else SetArchived(ArchivedEnum.Archived);
        }

        private void SetArchived(ArchivedEnum type)
        {
            try
            {
                this.mainVM.SetArchived(this.mainVM.SelectedNote, type);
                ComboBox_SelectionChanged(this.cmbox, null);
            }
            catch (Exception ex)
            {
                MainVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem item = (ComboBoxItem)this.cmbox.SelectedItem;
                switch (item.Content.ToString())
                {
                    case "Archived": this.mainVM.RefreshNotes(ArchivedEnum.Archived); break;
                    case "Not Archived": this.mainVM.RefreshNotes(ArchivedEnum.NotArchived); break;
                    case "All": this.mainVM.RefreshNotes(); break;
                }
            }
            catch (Exception ex)
            {
                MainVC.logFile.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
