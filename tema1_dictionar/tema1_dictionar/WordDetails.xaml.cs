using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace tema1_dictionar
{
    /// <summary>
    /// Interaction logic for WordDetails.xaml
    /// </summary>
    public partial class WordDetailsWindow : Window
    {
        public WordDetailsWindow()
        {
            InitializeComponent();
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
            wordImage.Source = new BitmapImage(new Uri(word.ImagePath));
        }

    }

}
