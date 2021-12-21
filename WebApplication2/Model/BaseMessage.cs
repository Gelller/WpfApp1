using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Model
{
    public abstract class BaseMessage
    {
        public string Body { get; set; }
        public string Name { get; set; }
    }
}
