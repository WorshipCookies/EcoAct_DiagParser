using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    class Choice(string ID, string choice, int karma_value) : TextContent
    {
        public string ID { get; set; } = ID;
        public string choice { set; get; } = choice;
        public int karma_value { set; get; } = karma_value;

    }
}
