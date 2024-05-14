using System.Text.Json.Serialization;

namespace DialogueConstructor
{
    public class Scene(int SceneID)
    {

        public int SceneID { get; set; } = SceneID;

        public List<TextContent> Dialogues { get; set; } = new List<TextContent>();

        public void enqueueDiag(TextContent text)
        {
            this.Dialogues.Add(text);
        }

    }

}
