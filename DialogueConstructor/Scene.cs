namespace DialogueConstructor
{
    class Scene
    {
        private int SceneID {  get; set; }
        private string SceneName { get; set; }
        
        private Queue<TextContent> Dialogues;

        public Scene(string SceneName)
        {
            this.SceneID = SceneID;
            this.SceneName = SceneName;
            this.Dialogues = new Queue<TextContent>();
        }


        public void enqueueDiag(TextContent text)
        {
            this.Dialogues.Enqueue(text);
        }

    }

}
