using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppArchitecture.Shared.Models
{
    public class Email
    {
        public string? ToEmailAddress { get; set; }
        public string? FromEmailAddress { get; set; }
        public string? Body { get; set; }
        public string? Subject { get; set; }
    }
}
