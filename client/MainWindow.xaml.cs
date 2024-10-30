using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.IO;
using System.Reflection.Emit;
using System.IO.Ports;
using Microsoft.Win32;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // If 0 - move for Player1, If 1 - for Player2
        private bool gameOver = false;

        // Set up communication speed same as server
        const int baud_rate = 115200;

        // Create chosen serial port object
        SerialPort? serial;

        Image[] Grid = new Image[9];

        DirectoryInfo basePath = new DirectoryInfo(AppContext.BaseDirectory);

        string pathO = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/circle.jpg";
        string pathX = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/cross.jpg";
        string pathBG = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/gBackground.jpg";
        BitmapImage imageX, imageO, imageBG;

        public MainWindow()
        {
            InitializeComponent();

            imageX = new BitmapImage(new Uri(pathX));
            imageO = new BitmapImage(new Uri(pathO));
            imageBG = new BitmapImage(new Uri(pathBG));
            resetGame();

            Grid[0] = g0;
            Grid[1] = g1;
            Grid[2] = g2;
            Grid[3] = g3;
            Grid[4] = g4;
            Grid[5] = g5;
            Grid[6] = g6;
            Grid[7] = g7;
            Grid[8] = g8;

            ConnectESP32();
        }

        
        private void g0MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_0"));
            checkForWin();
        }

        private void g1MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_1"));
            checkForWin();
        }

        private void g2MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_2"));
            checkForWin();
        }

        private void g3MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_3"));
            checkForWin();
        }

        private void g4MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_4"));
            checkForWin();
        }

        private void g5MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_5"));
            checkForWin();
        }

        private void g6MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_6"));
            checkForWin();
        }

        private void g7MouseUp(object sender, MouseButtonEventArgs e)
        {
             fillCell(ExecCommand("MW_7"));
            checkForWin();
        }

        private void g8MouseUp(object sender, MouseButtonEventArgs e)
        {
            fillCell(ExecCommand("MW_8"));
            checkForWin();
        }


        private void playGmeClick(object sender, RoutedEventArgs e)
        {
            canvGameMode.Visibility = Visibility.Visible;

            canvPlay.Visibility = Visibility.Hidden;
            canvInGame.Visibility = Visibility.Hidden;
        }
        private void loadGmeClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set options for the file dialog (optional)
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Select a File";

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                // Display the selected file path
                MessageBox.Show($"Selected file: {openFileDialog.FileName}");
            }
        }

        private void gameModeMMClick(object sender, RoutedEventArgs e)
        {
            canvInGame.Visibility = Visibility.Visible;

            canvPlay.Visibility = Visibility.Hidden;
            canvGameMode.Visibility = Visibility.Hidden;
            gameModeChoosen();
        }

        private void gameModeMAClick(object sender, RoutedEventArgs e)
        {
            canvInGame.Visibility = Visibility.Visible;

            canvPlay.Visibility = Visibility.Hidden;
            canvGameMode.Visibility = Visibility.Hidden;
            gameModeChoosen();
        }

        private void gameModeAAClick(object sender, RoutedEventArgs e)
        {
            canvInGame.Visibility = Visibility.Visible;

            canvPlay.Visibility = Visibility.Hidden;
            canvGameMode.Visibility = Visibility.Hidden;
            gameModeChoosen();
        }

        private void gameModeChoosen()
        {
            g0.IsEnabled = true;
            g1.IsEnabled = true;
            g2.IsEnabled = true;
            g3.IsEnabled = true;
            g4.IsEnabled = true;
            g5.IsEnabled = true;
            g6.IsEnabled = true;
            g7.IsEnabled = true;
            g8.IsEnabled = true;

            ExecCommand("RW");
        }

        private void mainMenuClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "You are about to throw this game",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                canvPlay.Visibility = Visibility.Visible;

                canvGameMode.Visibility = Visibility.Hidden;
                canvInGame.Visibility = Visibility.Hidden;

                string recCommand = ExecCommand("RW");
                if (recCommand[0] == 'R' && recCommand[1] == 'A')
                {
                    resetGame();
                }
            }
        }

        private void saveGmeClick(object sender, RoutedEventArgs e)
        {

        }
        private void ConnectESP32()
        {
            if (FindCH340ComPort() != "")
            {
                try
                {
                    string port = FindCH340ComPort();
                    serial = new SerialPort(port, baud_rate);
                    //l1.Content = $"Successfully connected on port: {port}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                "Failed to find ESP32. Retry?",
                "Warning",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ConnectESP32();
                }
                else
                {
                    //Application.Current.Shutdown();
                }
            }
        }

        private void resetGame()
        {
            g0.Source = imageBG;
            g1.Source = imageBG;
            g2.Source = imageBG;
            g3.Source = imageBG;
            g4.Source = imageBG;
            g5.Source = imageBG;
            g6.Source = imageBG;
            g7.Source = imageBG;
            g8.Source = imageBG;

            g0.IsEnabled = false;
            g1.IsEnabled = false;
            g2.IsEnabled = false;
            g3.IsEnabled = false;
            g4.IsEnabled = false;
            g5.IsEnabled = false;
            g6.IsEnabled = false;
            g7.IsEnabled = false;
            g8.IsEnabled = false;

            gameOver = false;
        }

        private void fillCell(string recCommand)
        {
            if (recCommand[0] == 'M' && recCommand[1] == 'A')
            {
                int cell = recCommand[3] - '0';
                Grid[cell].Source = recCommand[4] == 'x' ? imageX : imageO;
            }
        }

        private void checkForWin()
        {
            string recCommand = ExecCommand("WW");
            if (recCommand[0] == 'W' && recCommand[1] == 'A') 
            {
                string winMessage;
                if (recCommand[3] == 'd')
                    winMessage = "Draw!";
                else if (recCommand[3] == 'x')
                    winMessage = "Player1 Wins!";
                else
                    winMessage = "Player2 Wins!";
                gameOver = true;
                MessageBox.Show(winMessage, "Results", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string FindCH340ComPort()
        {
            try
            {
                // Use WMI to find USB devices and filter for "USB-SERIAL CH340" with status "OK"
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                {
                    foreach (ManagementObject device in searcher.Get())
                    {
                        string? name = device["Name"]?.ToString();
                        string? status = device["Status"]?.ToString();

                        // Check if the device name contains "USB-SERIAL CH340" and has "OK" status
                        if (name != null && name.Contains("USB-SERIAL CH340") && status == "OK")
                        {
                            // Use a regex pattern to extract the COM port number
                            var match = System.Text.RegularExpressions.Regex.Match(name, @"(COM\d+)");
                            if (match.Success)
                            {
                                return match.Value; // This will capture "COM*" where * is the port number
                            }
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("An error occurred while querying for WMI data: " + e.Message);
            }

            return ""; // Return null if no matching device is found
        }

        private string ExecCommand(string command)
        {
            if (serial != null)
            {
                try
                {
                    serial.Open();
                    serial.WriteLine(command);

                    return serial.ReadLine();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (serial.IsOpen)
                        serial.Close();
                }
            }
            return "Serial port isnt opened.";
        }
    } 
}