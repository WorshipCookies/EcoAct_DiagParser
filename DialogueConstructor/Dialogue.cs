using System.Reflection.PortableExecutable;

namespace DialogueConstructor
{
    class Dialogue : TextContent
    {
        private int ID { get; set; }
        private Queue<Speech> text { get; set; }

        private List<Consequence> consequences { get; set; }

        public Dialogue()
        {
            this.ID = 0;
            this.text = new Queue<Speech>();
        }

        public void addSpeech(string dialog, Character c)
        {
            checkRef(dialog);
            text.Enqueue(new Speech(dialog, c));
        }

        public void buildConsequences(string choice_id, string con_id)
        {
            consequences = new List<Consequence>();
        }

        public string checkRef(string dialog)
        {
            // Check Ref in Text
            return "";
        }
    }
}

