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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            InitializeAdminsList();
        }

        private List<Admin> admins;

        private void InitializeAdminsList()
        {
            string jsonFilePath = "D:\\facultate\\II\\SEM II\\MVP\\Dictionar-MVP\\tema1_dictionar\\tema1_dictionar\\input\\admin_list.json";

            if (File.Exists(jsonFilePath))
            {
                // Read the JSON file
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Deserialize JSON content into a list of Word objects
                admins = JsonConvert.DeserializeObject<List<Admin>>(jsonContent);
            }
            else
            {
                MessageBox.Show("Fișierul JSON nu există.");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Open the MainWindow window
            UserMainWindow userMainWindow = new UserMainWindow();
            userMainWindow.Show();
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Username" || textBox.Text == "Password")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Change the text color to black
            }
        }

        private bool ValidateAdmin(string username, string password)
        {
            foreach (Admin admin in admins)
            {
                if (admin.Username == username && admin.Password == password)
                {
                    return true; // Match found
                }
            }
            return false; // No match found
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            string username = userBox.Text;
            string password = passBox.Text;

            if (ValidateAdmin(username, password))
            {
                // Open the MainWindow window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                // Close the current window
                this.Close();
            }
            else
            {
                MessageBox.Show("Username sau parolă invalidă. Încercați din nou.");
            }
        }
    }
}
