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
    public partial class RemoveWord : Window
    {
        private List<Word> words;

        public RemoveWord()
        {
            InitializeComponent();
            words = WordManager.Instance.Words;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Cuvânt")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Change the text color to black
            }
        }
        private void CheckWord_Click(object sender, RoutedEventArgs e)
        {
            string wordToRemove = removeWordTextBox.Text;
            Word word = words.FirstOrDefault(w => w.Text == wordToRemove);

            if (word != null)
            {
                // Remove the word from the list
                words.Remove(word);
                // Save the modified list of words to the JSON file
                SaveWordsToJsonFile();

                MessageBox.Show("Cuvânt șters cu succes.");
            }
            else
            {
                MessageBox.Show("Cuvântul dat nu există în listă.");
            }
        }

        private void SaveWordsToJsonFile()
        {
            string jsonFilePath = "D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\tema1_dictionar\\input\\word_list.json";

            // Serialize the list of words to JSON format
            string jsonContent = JsonConvert.SerializeObject(words);

            // Write the JSON content to the file
            File.WriteAllText(jsonFilePath, jsonContent);
        }
    }
}
