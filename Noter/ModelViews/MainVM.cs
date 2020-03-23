using Noter.Models;
using Noter.Models.DbClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.ModelViews
{
    public class MainVM : ViewModel
    {
        private ObservableCollection<Note> notes;
        /// <summary>
        /// Collection of notes
        /// </summary>
        public ObservableCollection<Note> Notes
        {
            get { return notes; }
        }

        private Note selectedNote;
        /// <summary>
        /// Selected note
        /// </summary>
        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                this.selectedNote = value;
                NotifiyPropertyChanged("SelectedNote");
            }
        }

        /// <summary>
        /// Db connector
        /// </summary>
        private DbConnector dbConnector;

        public MainVM(DbConnector dbConnector)
        {
            this.dbConnector = dbConnector;
            this.notes = new ObservableCollection<Note>();
        }

        /// <summary>
        /// Remove note
        /// </summary>
        /// <param name="note"></param>
        public void RemoveNote(Note note)
        {
            using (NoterDbContext ctx = this.dbConnector.GetContext())
            {
                ctx.Entry(note).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Clear local notes and load database
        /// </summary>
        public void RefreshNotes()
        {
            this.Notes.Clear();
            using (NoterDbContext ctx = this.dbConnector.GetContext())
            {
                foreach (Note note in ctx.Notes)
                {
                    this.Notes.Add(note);
                }
            }
        }

        /// <summary>
        /// Set note by ArchivedEnum
        /// </summary>
        /// <param name="note"></param>
        /// <param name="type"></param>
        public void SetArchived(Note note, ArchivedEnum type)
        {
            using (NoterDbContext db = this.dbConnector.GetContext())
            {
                note.Archived = type;
                db.Entry(note).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Fill Notes by ArchivedEnum
        /// </summary>
        /// <param name="type"></param>
        public void RefreshNotes(ArchivedEnum type)
        {
            this.Notes.Clear();
            using (NoterDbContext ctx = this.dbConnector.GetContext())
            {
                foreach (Note note in ctx.Notes)
                {
                    if (note.Archived.Equals(type))
                        this.Notes.Add(note);
                }
            }
        }
    }
}
