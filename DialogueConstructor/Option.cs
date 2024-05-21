using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    public class Option(int option_ID, string option_text, int karma_value)
    {
        public int option_ID { get; set; } = option_ID;
        public string option_text { get; set; } = option_text;
        public int karma_value { get; set; } = karma_value;
    }
}
