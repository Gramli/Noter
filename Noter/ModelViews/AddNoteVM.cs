using Noter.Models;
using Noter.Models.DbClasses;
using System;

namespace Noter.ModelViews
{
    public class AddNoteVM : ViewModel
    {
        private string name;
        /// <summary>
        /// Note name
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                this.name = value;
                NotifiyPropertyChanged("Name");
            }
        }

        private string text;
        /// <summary>
        /// Note text
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                this.text = value;
                NotifiyPropertyChanged("Text");
            }
        }

        private PriorityEnum priority;
        /// <summary>
        /// Note priority
        /// </summary>
        public PriorityEnum Priority
        {
            get { return priority; }
            set
            {
                this.priority = value;
                NotifiyPropertyChanged("Priority");
            }
        }

        /// <summary>
        /// Note to edit
        /// </summary>
        private Note note;

        /// <summary>
        /// Database connector
        /// </summary>
        private DbConnector connector;

        /// <summary>
        /// Inicialize connector
        /// </summary>
        /// <param name="connector"></param>
        public AddNoteVM(DbConnector connector)
        {
            this.connector = connector;
        }

        /// <summary>
        /// Inicialize connector
        /// inicialize note to edit
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="note"></param>
        public AddNoteVM(DbConnector connector, Note note)
           : this(connector)
        {
            this.note = note;
            this.Text = note.Text;
            this.Name = note.Name;
            this.Priority = note.Priority;
        }

        /// <summary>
        /// Save or Edit note and save changes to database
        /// </summary>
        public void SaveChanges()
        {
            using (NoterDbContext db = this.connector.GetContext())
            {
                if (this.note != null)
                {
                    this.note.Name = this.Name;
                    this.note.Text = this.Text;
                    this.note.Priority = this.Priority;
                    this.note.Edited = DateTime.Now.ToString(Note.DateTimeFormat);
                    db.Entry(this.note).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    Note newNote = new Note(this.Name, this.Text, this.Priority);
                    db.Notes.Add(newNote);
                }

                db.SaveChanges();
            }
        }
    }
}
