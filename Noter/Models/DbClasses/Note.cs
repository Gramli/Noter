using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noter.Models.DbClasses
{
    /// <summary>
    /// Note table
    /// </summary>
    [Table("Note")]
    public class Note
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        [Column("Added")]
        public string Added { get; set; }
        [Column("Edited")]
        public string Edited { get; set; }
        [Column("Priority")]
        public PriorityEnum Priority { get; set; }
        [Column("Archived")]
        public ArchivedEnum Archived { get; set; }

        public static string DateTimeFormat { get { return "yyyy.MM.dd hh:mm:ss"; } }


        public Note(string name, string text, PriorityEnum priority)
        {
            this.Name = name;
            this.Text = text;
            this.Priority = priority;
            this.Added = DateTime.Now.ToString(Note.DateTimeFormat);
        }

        public Note()
        { }
    }
}
