using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    [JsonDerivedType(typeof(Dialogue))]
    public class Choice(string ID, string ChoiceText) : TextContent
    {
        public string ID { get; set; } = ID;
        public string ChoiceText { get; set; } = ChoiceText;
        public List<Option> Options { get; set; } = new List<Option>();

        public void addOption(int id, string option, int karma_value) 
        { 
            Options.Add(new Option(id, option, karma_value));
        }

    }
}
