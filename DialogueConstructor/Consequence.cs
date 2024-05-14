using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    public class Consequence
    {
        public string choice_id {  get; set; }
        public string consequence_id { get; set; }
        public List<Tuple<int, string, int>> consequences { get; set; }

        // Construtor Class
        public Consequence(string choice_id, string consequence_id) 
        { 
            this.choice_id = choice_id;
            this.consequence_id = consequence_id;

            consequences = new List<Tuple<int, string, int>>();
        }

        // Adder
        public void addConsequence(Tuple<int, string, int> con)
        {
            this.consequences.Add(con);
        }

    }
}
