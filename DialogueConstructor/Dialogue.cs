using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;

namespace DialogueConstructor
{
    public class Dialogue() : TextContent("Dialogue")
    {
        public int ID { get; set; } = 0;
        
        public List<Speech> text { get; set; } = [];

        public List<Consequence> consequences { get; set; } = [];

        public void addSpeech(string dialog, Character c)
        {
            text.Add(new Speech(dialog, c));
        }

        public void addConsequences(Consequence consequence)
        {
            consequences.Add(consequence);
        }

        public string checkRef(string dialog)
        {
            // Check Ref in Text
            return "";
        }
    }
}

