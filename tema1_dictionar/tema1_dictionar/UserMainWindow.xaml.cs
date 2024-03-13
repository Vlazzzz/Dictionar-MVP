using System.IO;
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


        public UserMainWindow()
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
            }
        }

        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Text == "Cuvânt")
            {
                comboBox.Text = string.Empty;
                comboBox.Foreground = Brushes.Black; // Change the text color to black
            }
        }

        private void ComboBox_KeyDown(object sender, KeyEventArgs e)
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Word not found.");
            }
        }

        private void ImageButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Handle the button click event here
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}