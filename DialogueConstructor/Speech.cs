using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    public class Speech(string text, Character C)
    {
        public Character character { get; set; } = C;
        public string text { get; set; } = text;
    }
}
