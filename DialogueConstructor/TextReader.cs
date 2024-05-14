using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DialogueConstructor
{
    public class TextReader(String path) : IDisposable
    {
        private StreamReader sr = new StreamReader(path, Encoding.UTF8, true);

        public Scene TextParser(int sceneID)
        {
            Scene scene = new Scene(sceneID);
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
                        scene.enqueueDiag(BuildNarrator());
                    }
                    else if (line == "[Dialogue]")
                    {
                        // Treat Dialogue Tag
                        scene.enqueueDiag(BuildDialogue());
                    }
                    else if (line == "[Choice]")
                    {
                        // Treat Choice Tag
                        scene.enqueueDiag(BuildChoice());
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

            sr.Close();
            return scene;
        }

        /// <summary>
        /// Creates a Choice with Options and Returns it
        /// </summary>
        /// <returns>Returns a Choice with Options</returns>
        /// <exception cref="NullReferenceException"></exception>
        private Choice BuildChoice()
        {

            // First line of Choice is always text!
            Choice choice = null;
            string choicetext = sr.ReadLine();
            
            // Continue Cycle
            string line = sr.ReadLine();

            while (line != null && line != "[\\Choice]")
            {
                line = line.Trim();
                if (line[0] == '[')
                {
                    // Tag was detected 
                    // Extract the Tag
                    string tag = line.Substring(1, line.Length - 2);

                    if (tag[0] != '\\')
                    {
                        // Extract Choice ID and create choice
                        choice = new Choice(tag, choicetext);
                    }
                    else
                    {
                        // Do Nothing - Its the end of the Choice
                    }
                }
                else
                {
                    // Check and see if Choice is Null
                    if (choice == null)
                    {
                        throw new NullReferenceException("Choice Detected but no ID Tag Exists!");
                    }

                    // Choice Options
                    
                    // First Split Option ID and Text_Karma
                    string[] splitter = line.Split(':');
                    int option_id = Int32.Parse(splitter[0]);

                    // Split Text and Karma
                    string[] text_karma = splitter[1].Split('_');
                    string choice_text = text_karma[0].Trim();
                    int karma = Int32.Parse(text_karma[1]);

                    // Add Option Values
                    choice.addOption(option_id, choice_text, karma);
                }
                line = sr.ReadLine(); // Continue Cycle
            }
            return choice;
        }

        /// <summary>
        /// This Function Builds the Simple Narrator Text Blocks.
        /// </summary>
        /// <returns>Dialogue Type</returns>
        private Dialogue BuildNarrator()
        {
            Dialogue dialogue = new Dialogue();
            string narrator_text = "";
            string line = sr.ReadLine();
            while((line != null && line != "[\\Narrator]"))
            {
                narrator_text += line.Trim()+"\n";
                line = sr.ReadLine();
            }
            dialogue.addSpeech(narrator_text, new Character("Narrator", "C_NAR"));
            return dialogue;
        } 

        /// <summary>
        /// This Section Builds Dialogue and consequences related to it.
        /// </summary>
        /// <returns>Dialogue Type with Character Speech</returns>
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
                else if (!String.IsNullOrWhiteSpace(line))
                {
                    dialogue.addSpeech(line, new Character("Narrator", "C_NAR"));
                }

                line = sr.ReadLine();

            }
            return dialogue;
        }

        /// <summary>
        /// Builds a Consequence Object from the Text and attaches it to the Dialogue. 
        /// </summary>
        /// <param name="d">Dialogue to Add the Consequence to</param>
        private void DealConsequence(Dialogue d)
        {
            Consequence consequence = null;
            string line = sr.ReadLine();
            while ((line != null && line != "[\\Consequence]"))
            {
                line = line.Trim();
                if (line[0] == '[')
                {
                    // Extract the Tag
                    string tag = line.Substring(1, line.Length - 2);

                    if (tag[0] != '\\')
                    {
                        // Extract Choice ID 
                        // Extract Consequence ID
                        string[] split = tag.Split(".");
                        consequence = new Consequence(split[0], split[1]);
                    }
                    else
                    {
                        d.addConsequences(consequence);
                    }
                }
                else
                {
                    // Build Consequence List
                    string[] split = line.Split(":");
                    int option_id = Int32.Parse(split[0]);
                    string text_option = split[1].Split("_")[0];
                    int karma = Int32.Parse(split[1].Split("_")[1]);
                    consequence.addConsequence(new Tuple<int, string, int>(option_id,text_option,karma));
                }

                line = sr.ReadLine(); // Read Next Line
            }
        }


        private static bool isCharacter(string line)
        {
            string[] checker = line.Split(null);
            return checker[0].Split('_')[0] == "C";
        }

        //private static TextContent LineCoder(string line)
        //{
            
        //    // Parse first Line
        //    string[] split = line.Split(null);
        //    if (CheckChoice(split[0]) == 0)
        //    {
        //        // Parse Choice
        //        int KARMA_VALUE_TEMP = 1;
        //        return new Choice(split[0][..^1], split[1], KARMA_VALUE_TEMP);
        //    }
        //    else if (CheckChoice(split[0]) == 1)
        //    {
        //        // Parse Normal Dialogue with Character
        //        string cName = split[0].Split('_')[1];
        //        return new Dialogue(split[1], cName);

        //    }
        //    else
        //    {
        //        // Parse Narrator Dialogue
        //        return new Dialogue(split[1], "Nar");
        //    }
        //}

        /**
         * Checks to see what type of Dialogue it is.
         * 0 = Choice Dialogue
         * 1 = Character Dialogue
         * 2 = Narrator
         */
        //private static int CheckChoice(string isChoice)
        //{
        //    if (isChoice[0] == 'C' || isChoice[0] == 'c')
        //    {
        //        return 0;
        //    }
        //    else if (isChoice[0] == 'H' || isChoice[0] == 'h')
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 2;
        //    }
        //}

        public void Dispose()
        {
            sr.Dispose();
            sr.Close();
        }
    }
}
