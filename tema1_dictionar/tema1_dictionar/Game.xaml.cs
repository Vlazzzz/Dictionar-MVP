using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace tema1_dictionar
{
    public partial class Game : Window
    {
        private List<Word> words;
        private List<Word> pickedWords = new List<Word>();
        private Random random;
        private int currentRound;
        private bool isDescriptionHint;
        private int _correctCounter = 0;

        public Game()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            random = new Random();
            words = WordManager.Instance.Words;
            currentRound = 1;
            LoadNextRound();
        }

        private void LoadNextRound()
        {
            checkWord.IsEnabled = true;
            isDescriptionHint = random.Next(2) == 0; // Randomly choose whether to use description or image hint

            // Update round textBlock
            round.Text = $"{currentRound}/5";

            Word selectedWord;

            // Loop until a new word is selected
            do
            {
                // Randomly select a word
                selectedWord = words[random.Next(words.Count)];

                // If the word was already picked or its image path is not appropriate and isDescriptionHint is false, continue to select another word
            } while (pickedWords.Contains(selectedWord) || (!isDescriptionHint && selectedWord.ImagePath == "D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\Resurse\\no_image.png"));

            pickedWords.Add(selectedWord); // Add the selected word to pickedWords

            // Set hint based on isDescriptionHint
            if (isDescriptionHint)
            {
                // Use description as hint
                wordDescription.Text = selectedWord.Description;
                wordImage.Source = null;
            }
            else
            {
                // Use image as hint
                wordDescription.Text = string.Empty; // Clear description
                wordImage.Source = new BitmapImage(new Uri(selectedWord.ImagePath));
            }

            if (currentRound == 5)
            {
                nextRoundButton.IsEnabled = false;
            }
        }


        private void checkWord_Click_1(object sender, RoutedEventArgs e)
        {
            string guessedWord = wordInsert.Text.Trim().ToLower();
            if (guessedWord == "")
            {
                MessageBox.Show("Introduceți un cuvânt.");
                return;
            }

            Word selectedWord = words.FirstOrDefault(w => w.Text.ToLower() == guessedWord);

            if (selectedWord != null)
            {
                // Word guessed correctly
                _correctCounter++;
                verifyImage.Source = new BitmapImage(new Uri("D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\Resurse\\mark.png"));
            }
            else
            {
                // Word guessed incorrectly
                verifyImage.Source = new BitmapImage(new Uri("D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\Resurse\\red_mark.png"));
            }
            checkWord.IsEnabled = false;
            //daca este ultima runda
            if (currentRound == 5)
            {
                // Close the game window after 1 second
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    // Game over
                    MessageBox.Show($"Jocul s-a terminat!\nAți avut {_correctCounter}/5 răspunsuri corecte.");
                    this.Close();
                };
                timer.Start();
            }
        }

        private void NextRoundButton_Click(object sender, RoutedEventArgs e)
        {
            string guessedWord = wordInsert.Text.Trim();
            if (guessedWord == "")
            {
                MessageBox.Show("Introduceți un cuvânt.");
                return;
            }

            if (currentRound < 5)
            {
                currentRound++;
                LoadNextRound();
                verifyImage.Source = null;
            }
            else
            {
                // Game over
                MessageBox.Show($"Jocul s-a terminat!\nAți avut {_correctCounter}/5 răspunsuri corecte.");
                this.Close();
            }
            wordInsert.Text = string.Empty;
        }
    }
}
//inca apar cuvinte fara poza
//de implementat sa nu se repete cuvintele