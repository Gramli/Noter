using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.Models
{
    /// <summary>
    /// Connecto to db
    /// </summary>
    public class DbConnector
    {
        public DbConnector()
        {
        }

        /// <summary>
        /// Get db context
        /// </summary>
        /// <returns></returns>
        public NoterDbContext GetContext()
        {
            return new NoterDbContext();
        }
    }
}
