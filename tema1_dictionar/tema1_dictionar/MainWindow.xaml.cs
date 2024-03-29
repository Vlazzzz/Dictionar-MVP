﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Windows.Navigation;

//de uitat peste autoComplete

namespace tema1_dictionar
{
    public partial class MainWindow : Window
    {
        private List<Word> words;
        private bool searchByWord = true;

        public MainWindow()
        {
            InitializeComponent();
            words = WordManager.Instance.Words;
            if (!searchByWord)
            {
                myComboBox.ItemsSource = words.Select(w => w.Category);
            }
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Text == "Cuvânt" || comboBox.Text == "Categorie")
            {
                comboBox.Text = string.Empty;
                comboBox.Foreground = Brushes.Black; // Change the text color to black

                // Populate the drop-down list with categories if searching by category
                if (!searchByWord)
                {
                    var categories = words.Select(w => w.Category).Distinct();
                    myComboBox.ItemsSource = categories;
                }
                else
                {
                    // Clear the auto-complete suggestions
                    myComboBox.ItemsSource = null;
                }
            }
        }

        private void ComboBox_KeyDown_searchWords(object sender, KeyEventArgs e)
        {
            if (searchByWord)
            {
                myComboBox.IsDropDownOpen = true;

                ComboBox comboBox = (ComboBox)sender;
                string searchText = comboBox.Text.ToLower(); // Retrieve text from the ComboBox

                // If the search text is empty, do not perform search
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Clear the auto-complete suggestions
                    myComboBox.ItemsSource = null;
                    return;
                }

                // Filter words that start with the search text
                var matchedWords = words.Where(w => w.Text.ToLower().StartsWith(searchText)).Select(w => w.Text);
                // Display matched words as auto-complete options
                myComboBox.ItemsSource = matchedWords;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchByWord)
            {
                // Retrieve the text from the ComboBox
                string searchText = myComboBox.Text.Trim();

                // Find the word in the list of words
                Word selectedWord = words.FirstOrDefault(w => w.Text.Equals(searchText, StringComparison.OrdinalIgnoreCase));

                if (selectedWord != null)
                {
                    // Display the word
                    wordNameBlock.Text = $"{selectedWord.Text}";

                    // Display the category
                    wordDescriptionBlock.Text = $"Categorie: {selectedWord.Category}";

                    // Append the description to the wordTextBlock
                    wordDescriptionBlock.Text += $" - {selectedWord.Description}";

                    // Display the image
                    wordImageBlock.Source = new BitmapImage(new Uri(selectedWord.ImagePath));
                }
                else
                {
                    // Word not found, display a message or handle the case accordingly
                    MessageBox.Show("Cuvântul nu există.");
                }
            }
            else
            {
                // Retrieve the selected category from the ComboBox
                string selectedCategory = myComboBox.Text.Trim();

                // Filter the list of words based on the selected category
                var wordsInCategory = words.Where(w => w.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));

                if (wordsInCategory.Any())
                {
                    // Populate the ComboBox with words from the selected category
                    myComboBox.ItemsSource = wordsInCategory.Select(w => w.Text);
                    searchByWord = true;
                    searchFilter.Content = "Categorie";
                }
                else
                {
                    MessageBox.Show("Nu s-au găsit cuvinte în această categorie.");
                }
            }
        }

        private void AddWord_Click(object sender, RoutedEventArgs e)
        {
            AddWord addWordWindow = new AddWord();
            addWordWindow.Show();
        }

        private void ModifyWord_Click(object sender, RoutedEventArgs e)
        {
            ModifyWord modifyWordWindow = new ModifyWord();
            modifyWordWindow.Show();
        }

        private void RemoveWord_Click(object sender, RoutedEventArgs e)
        {
            RemoveWord removeWordWindow = new RemoveWord();
            removeWordWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (searchByWord == true)
            {
                myComboBox.Text = "Categorie";
                searchByWord = false;
                searchFilter.Content = "Cuvânt";
            }
            else
            {
                myComboBox.Text = "Cuvânt";
                searchByWord = true;
                searchFilter.Content = "Categorie";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //navigate to the game window
            Game gameWindow = new Game();
            gameWindow.Show();
        }
    }
}