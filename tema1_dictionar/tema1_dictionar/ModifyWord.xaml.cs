using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Newtonsoft.Json;

namespace tema1_dictionar
{
    public partial class ModifyWord : Window
    {
        private Word selectedWord;
        private List<Word> words;

        public ModifyWord()
        {
            InitializeComponent();
            words = WordManager.Instance.Words;
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

        private void ModifyWords()
        {
            // Check if a word is selected
            if (!string.IsNullOrEmpty(modifyWord.Text))
            {
                // Find the word in the list
                selectedWord = words.FirstOrDefault(word => word.Text == modifyWord.Text);

                // Check if the selected word exists in the list
                if (selectedWord != null)
                {
                    // Update the word details
                    selectedWord.Category = modifyCategory.Text;
                    selectedWord.Description = modifyDescription.Text;
                    selectedWord.ImagePath = modifyImage.Text;

                    // Save the modified list of words to the JSON file
                    SaveWordsToJsonFile();

                    // Display a success message or perform any other necessary actions
                    MessageBox.Show("Cuvânt modificat cu succes.");
                }
                else
                {
                    // Display an error message if the selected word does not exist in the list
                    MessageBox.Show("Cuvântul dat încă nu a fost înregistrat.");
                }
            }
            else
            {
                // Display an error message if no word is entered
                MessageBox.Show("Introduceți un cuvânt.");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ModifyWords();
        }

        private void CheckWord_Click(object sender, RoutedEventArgs e)
        {
            string wordToCheck = modifyWord.Text;
            Word word = words.FirstOrDefault(w => w.Text == wordToCheck);
            if (word != null)
            {
                // Populate other text boxes with word details
                modifyCategory.Text = word.Category;
                modifyDescription.Text = word.Description;
                modifyImage.Text = word.ImagePath;

                modifyCategory.Foreground = Brushes.Black;
                modifyDescription.Foreground = Brushes.Black;
                modifyImage.Foreground = Brushes.Black;
            }
            else
            {
                MessageBox.Show("Cuvântul dat nu există în listă.");
            }
        }
    }
}
