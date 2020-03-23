using Noter.Models.DbClasses;
using System.Data.Entity;

namespace Noter.Models
{
    /// <summary>
    /// Noter databse context
    /// </summary>
    public class NoterDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
    }
}
