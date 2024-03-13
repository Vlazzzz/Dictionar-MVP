using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace tema1_dictionar
{
    public partial class AddWord : Window
    {
        // Define event
        public event WordAddedEventHandler WordAdded;
        private List<Word> words;

        public delegate void WordAddedEventHandler(object sender, EventArgs e);
        public AddWord()
        {
            InitializeComponent();
            InitializeWordsList();
        }

        private void InitializeWordsList()
        {
            string jsonFilePath = "D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\tema1_dictionar\\input\\word_list.json";

            if (File.Exists(jsonFilePath))
            {
                // Read the JSON file
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Deserialize JSON content into a list of Word objects
                words = JsonConvert.DeserializeObject<List<Word>>(jsonContent);
            }
            else
            {
                MessageBox.Show("JSON file not found.");
                Close(); // Close the window if JSON file is not found
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Cuvânt" || textBox.Text == "Categorie" || textBox.Text == "Descriere")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Change the text color to black
            }
        }

        private void AddWordToList()
        {
            string newWordText = addWord.Text.Trim();
            string newCategory = addCategory.Text.Trim();
            string newDescription = addDescription.Text.Trim();
            string newImagePath = addImage.Text.Trim();

            if (newWordText == "Cuvânt" || newCategory == "Categorie" || newDescription == "Descriere")
            {
                MessageBox.Show("Vă rog să completați toate câmpurile.");
                return;
            }

            // Check if the word already exists in the list
            if (words.Any(w => w.Text.Equals(newWordText, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Cuvântul deja există.");
                return;
            }

            // Add the new word to the list
            words.Add(new Word { Text = newWordText, Category = newCategory, Description = newDescription, ImagePath = newImagePath});

            // Serialize the updated list of words to JSON format
            string jsonContent = JsonConvert.SerializeObject(words, Formatting.Indented);

            // Write the JSON content to the file
            File.WriteAllText("D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\tema1_dictionar\\input\\word_list.json", jsonContent);

            MessageBox.Show("Cuvântul a fost înregistrat cu succes.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWordToList();
            OnWordAdded(EventArgs.Empty);
        }

        protected virtual void OnWordAdded(EventArgs e)
        {
            // Raise the event
            WordAdded?.Invoke(this, e);
        }
    }
}
