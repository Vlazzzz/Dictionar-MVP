using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace tema1_dictionar
{
    public partial class MainWindow : Window
    {
        private List<Word> words;
        public MainWindow()
        {
            InitializeComponent();

            // Initialize the list of words
            Word portocala = new Word{ Text = "Portocala", Description = "Fructul portocalului, de formă sferică, aromat și zemos, bogat în vitamine, învelit într-o coajă de culoare galbenă-roșiatică.", Category = "Fructe", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\portocala.png" };
            Word masina = new Word { Text = "Mașină", Description = "Vehicul cu motor, cu tracțiune proprie, destinat transportului de persoane sau de mărfuri.", Category = "Transport", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\masina.png" };
            Word caine = new Word { Text = "Câine", Description = "Mamifer domestic, de talie mijlocie sau mare, cu blana variată, care se hrănește cu carne și care este folosit ca paznic, vânător sau animal de companie.", Category = "Animale", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\caine.png" };
            Word pisica = new Word { Text = "Pisică", Description = "Mamifer domestic, cu blana moale și variată, care se hrănește cu carne și care este folosit ca vânător de șoareci sau animal de companie.", Category = "Animale", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\pisica.png" };
            Word casa = new Word { Text = "Casă", Description = "Clădire destinată locuirii oamenilor.", Category = "Locuințe", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\casa.png" };
            Word telefon = new Word { Text = "Telefon",  Description = "Telecomunicație în care se realizează convorbiri la distanță prin mijlocirea undelor electromagnetice propagate de-a lungul unor fire; ansamblul instalațiilor necesare pentru acest scop.", Category = "Electronice", ImagePath = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\telefon.png" };

            words = new List<Word> { portocala, masina, caine, pisica, casa, telefon };
            // Write the words to a file
            string path = "D:\\facultate\\II\\SEM II\\MVP\\tema1_dictionar\\Resurse\\cuvinte.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (Word word in words)
                {
                    sw.WriteLine(word.Text);
                    sw.WriteLine(word.Description);
                    sw.WriteLine(word.Category);
                    sw.WriteLine(word.ImagePath);
                }
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
                // Create a new instance of WordDetailsWindow
                //WordDetailsWindow wordDetailsWindow = new WordDetailsWindow();

                // Set the properties of WordDetailsWindow to display the word's information
                //wordDetailsWindow.SetWordDetails(selectedWord);

                // Show the WordDetailsWindow
                //wordDetailsWindow.Show();
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

        public void SetWordDetails(Word word)
        {
            // Display the word
            wordNameBlock.Text = $"{word.Text}";

            // Display the category
            wordDescriptionBlock.Text = $"Categorie: {word.Category}";

            // Append the description to the wordTextBlock
            wordDescriptionBlock.Text += $" - {word.Description}";

            // Display the image
           // wordImage.Source = new BitmapImage(new Uri(word.ImagePath));
        }
    }
}