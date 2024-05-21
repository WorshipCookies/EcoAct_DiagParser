using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    public class TextContent(string diag)
    {

        public string DiagType { get; set; } = diag;
    }
}
