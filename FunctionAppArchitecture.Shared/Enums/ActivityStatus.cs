using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.Shared.Enums
{
    /// <summary>
    /// An enum for keeping track of the progress of the registration workflow
    /// </summary>
    public enum ActivityStatus
    {
        NotYetRun = 1,
        Succeeded = 2,
        Failed = 3,
        TimedOut = 4
    }
}
