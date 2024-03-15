using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace tema1_dictionar
{
    public class WordManager
    {
        private static WordManager instance;
        public List<Word> Words { get; private set; }

        private WordManager()
        {
            InitializeWordsList();
        }

        public static WordManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WordManager();
                }
                return instance;
            }
        }

        private void InitializeWordsList()
        {
            string jsonFilePath = "D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\tema1_dictionar\\input\\word_list.json";

            if (File.Exists(jsonFilePath))
            {
                // Read the JSON file
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Deserialize JSON content into a list of Word objects
                Words = JsonConvert.DeserializeObject<List<Word>>(jsonContent);
            }
            else
            {
                //MessageBox.Show("Fișierul JSON nu s-a găsit.");
            }
        }
    }
}