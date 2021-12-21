using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public class Message : BaseMessage
    {
        public string Subject { get; set; }    
        public string To { get; set; }
        public bool IsHtml { get; set; }

    }
}
