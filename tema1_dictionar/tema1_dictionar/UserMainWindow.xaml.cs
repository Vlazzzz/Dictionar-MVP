﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Windows.Navigation;

namespace tema1_dictionar
{
    public partial class UserMainWindow : Window
    {

        private List<Word> words;
        private bool searchByWord = true;


        public UserMainWindow()
        {
            InitializeComponent();
            InitializeWordsList();
            if (!searchByWord)
            {
                myComboBox.ItemsSource = words.Select(w => w.Category);
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
                words = JsonConvert.DeserializeObject<List<Word>>(jsonContent);
            }
            else
            {
                MessageBox.Show("Fișierul JSON nu există.");
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

        //eu vreau ca atunci cand selectez butonul de categorie, auto_completul sa imi arate toate cuvintele din categoria respectiva
        //dar daca selectez butonul de cuvant, sa imi arate toate cuvintele care se potrivesc cu ce am scris in casuta de cautare

        //totodata, daca butonul de cautare este pe categorie, vreau ca autocomplete-ul sa mi arata categoriile ce dau match cu ce am scris eu

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

        private void ImageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Handle the button click event here
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

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