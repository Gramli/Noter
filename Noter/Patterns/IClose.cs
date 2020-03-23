using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noter.Patterns
{
    /// <summary>
    /// IClose pattern
    /// </summary>
    public interface IClose
    {
        event CancelEventHandler Close;
    }
}
