using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    internal class TextReader(String path) : IDisposable
    {
        private StreamReader sr = new StreamReader(path);

        private Scene TextParser()
        {
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    // Check for Tags
                    line = line.Trim();
                    if (line == "[Narrator]")
                    {
                        // Treat Narrator Tag
                    }
                    else if (line == "[Dialogue]")
                    {
                        // Treat Dialogue Tag
                    }
                    else if (line == "[Choice]")
                    {
                        // Treat Choice Tag

                    }
                    else
                    {
                        // Ignore Text
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return null;
        }

        private Dialogue BuildNarrator()
        {
            Dialogue dialogue = new Dialogue();
            string narrator_text = "";
            string line = sr.ReadLine();
            while((line != null && line != "[\\Narrator]"))
            {
                narrator_text += line.Trim()+"\n";
            }
            dialogue.addSpeech(narrator_text, new Character("Narrator", "C_NAR"));
            return dialogue;
        } 

        private Dialogue BuildDialogue()
        {
            Dialogue dialogue = new Dialogue();
            string line = sr.ReadLine();

            while ((line != null && line != "[\\Dialogue]"))
            {
                line = line.Trim();
                // Check if Character Dialogue or Narrator
                if (isCharacter(line))
                {
                    string[] diag = line.Split(':');
                    dialogue.addSpeech(diag[1].Trim(),
                        new Character(diag[0].Split("_")[1], diag[0]));
                }
                else if (line == "[Consequence]")
                {
                    // Deal with Consequences
                    DealConsequence(dialogue);
                }
                else
                {
                    dialogue.addSpeech(line, new Character("Narrator", "C_NAR"));
                }

            }
            return dialogue;
        }

        private void DealConsequence(Dialogue d)
        {
            string line = sr.ReadLine();
            while ((line != null && line != "[\\Consequence]"))
            {
                // Extract Choice ID 


                // Extract Consequence ID

                // Build Consequence List
            }
        }


        private static bool isCharacter(string line)
        {
            string[] checker = line.Split(null);
            return checker[0].Split('_')[0] == "C";
        }

        private static TextContent LineCoder(string line)
        {
            
            // Parse first Line
            string[] split = line.Split(null);
            if (CheckChoice(split[0]) == 0)
            {
                // Parse Choice
                int KARMA_VALUE_TEMP = 1;
                return new Choice(split[0][..^1], split[1], KARMA_VALUE_TEMP);
            }
            else if (CheckChoice(split[0]) == 1)
            {
                // Parse Normal Dialogue with Character
                string cName = split[0].Split('_')[1];
                return new Dialogue(split[1], cName);

            }
            else
            {
                // Parse Narrator Dialogue
                return new Dialogue(split[1], "Nar");
            }
        }

        /**
         * Checks to see what type of Dialogue it is.
         * 0 = Choice Dialogue
         * 1 = Character Dialogue
         * 2 = Narrator
         */
        private static int CheckChoice(string isChoice)
        {
            if (isChoice[0] == 'C' || isChoice[0] == 'c')
            {
                return 0;
            }
            else if (isChoice[0] == 'H' || isChoice[0] == 'h')
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public void Dispose()
        {
            sr.Dispose();
            sr.Close();
        }
    }
}
