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
using System.Runtime.Versioning;
using System.Xml;
using System;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Window
    {
        // Uninitialized Serial port
        SerialPort? serial;
        // Communication speed with server
        const int baud_rate = 115200;


        // Base app directory
        DirectoryInfo basePath = new DirectoryInfo(AppContext.BaseDirectory);

        // Grid for cells
        Image[] Grid = new Image[9];

        // Images for cells
        string pathO = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/circle.jpg";
        string pathX = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/cross.jpg";
        string pathBG = new DirectoryInfo(AppContext.BaseDirectory).Parent?.Parent?.Parent + "/media/gBackground.jpg";
        BitmapImage imageX, imageO, imageBG;

        // Game mode
        int gameMode = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Setup images for cell
            imageX = new BitmapImage(new Uri(pathX));
            imageO = new BitmapImage(new Uri(pathO));
            imageBG = new BitmapImage(new Uri(pathBG));
            // Reset grid to init state
            resetGame();

            // Assign image components to grid
            Grid[0] = g0;
            Grid[1] = g1;
            Grid[2] = g2;
            Grid[3] = g3;
            Grid[4] = g4;
            Grid[5] = g5;
            Grid[6] = g6;
            Grid[7] = g7;
            Grid[8] = g8;

            // Connect to Server
            ConnectESP32();
        }

        // Grid cell click handlers
        private void g0MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(0);
        }

        private void g1MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(1);
        }

        private void g2MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(2);
        }

        private void g3MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(3);
        }

        private void g4MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(4);
        }

        private void g5MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(5);
        }

        private void g6MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(6);
        }

        private void g7MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(7);
        }

        private void g8MouseUp(object sender, MouseButtonEventArgs e)
        {
            makeMove(8);
        }

        // Make move based on gamemode
        // 2 Gamemode(AI vs AI) moves taken separately as different case in gameModeAA function
        private void makeMove(int cell)
        {
            if (gameMode == 0)
            {
                fillCell(ExecCommand($"MW_{cell}"));
                checkForWin();
            }
            else if (gameMode == 1) 
            {
                fillCell(ExecCommand($"MW_{cell}"));
                if (checkForWin())
                    return;
                fillCell(ExecCommand("MW"));
                checkForWin();
            }
        }
        
        // Fill cell of grid
        private void fillCell(string recCommand)
        {
            if (recCommand[0] == 'M' && recCommand[1] == 'A')
            {
                int cell = recCommand[3] - '0';
                Grid[cell].Source = recCommand[4] == 'x' ? imageX : imageO;
            }
        }

        private void playGmeClick(object sender, RoutedEventArgs e)
        {
            string recCommand = ExecCommand("RW");

            if (recCommand[0] == 'R' && recCommand[1] == 'A')
            {
                canvGameMode.Visibility = Visibility.Visible;

                canvPlay.Visibility = Visibility.Hidden;
                canvInGame.Visibility = Visibility.Hidden;
            }
        }
        
        // Save game into xml file
        private void saveGmeClick(object sender, RoutedEventArgs e)
        {
            string recCommand = ExecCommand("SW");
            if (recCommand[0] == 'S' && recCommand[1] == 'A') {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save Game State",
                    Filter = "XML Files (*.xml)|*.xml",
                    DefaultExt = "xml",
                    FileName = "GameSave.xml"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    if (!saveGameToFile(filePath, recCommand))
                    {
                        MessageBox.Show("Failed to save the game state.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // Load game from xml file
        private void loadGmeClick(object sender, RoutedEventArgs e)
        {
            string Data;

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Load Game State",
                Filter = "XML Files (*.xml)|*.xml",
                DefaultExt = "xml"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (loadGameFromFile(filePath, out Data))
                {
                    string recCommand = ExecCommand(Data);
                    if (recCommand[0] == 'L' && recCommand[1] == 'A')
                    {
                        for (int i = 5; i < 14; i++)
                        {
                            if (Data[i] == 'x')
                                Grid[i-5].Source = imageX;
                            else if (Data[i] == 'o')
                                Grid[i-5].Source = imageO;
                            else
                                Grid[i-5].Source = imageBG;
                        }

                        gameMode = Data[3] - '0';

                        SetGameMode(gameMode);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to load the game state.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool saveGameToFile(string filePath, string response)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("GameState");

                    writer.WriteElementString("GameMode", response[3].ToString());

                    writer.WriteElementString("CurrentMove", response[4].ToString());

                    writer.WriteStartElement("Grid");
                    for (int i = 5; i < 14; i++)
                    {
                        writer.WriteElementString("Cell", response[i].ToString());
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndDocument();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool loadGameFromFile(string filePath, out string DataToSent)
        {
            DataToSent = "LW_";
            try
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    while (reader.Read()) 
                    {
                        switch (reader.Name)
                        {
                            case "GameMode":
                                DataToSent += reader.ReadElementContentAsString();
                                break;
                            case "CurrentMove":
                                DataToSent += reader.ReadElementContentAsString();
                                break;
                            case "Cell":
                                DataToSent += (reader.ReadElementContentAsString());
                                break;
                        }
                    }
                }
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void gameModeMMClick(object sender, RoutedEventArgs e)
        {
            SetGameMode(0);
        }

        private void gameModeMAClick(object sender, RoutedEventArgs e)
        {
            SetGameMode(1);
        }

        private void gameModeAAClick(object sender, RoutedEventArgs e)
        {
            if (SetGameMode(2))
            {
                while (true)
                {
                    fillCell(ExecCommand("MW"));
                    if (checkForWin())
                        break;
                }
            }
        }

        // Set gamemode and clear grid.
        private bool SetGameMode(int mode)
        {
            string recCommand = ExecCommand($"GW_{mode}");
            
            if (recCommand[0] == 'G' && recCommand[1] == 'A')
            {
                gameMode = mode;
                switch (gameMode)
                {
                    case 0:
                        p1Name.Content = "P1: Man";
                        p2Name.Content = "P2: Man";
                        break;
                    case 1:
                        p1Name.Content = "P1: Man";
                        p2Name.Content = "P2: AI";
                        break;
                    case 2:
                        p1Name.Content = "P1: AI";
                        p2Name.Content = "P2: AI";
                        break;
                }

                canvInGame.Visibility = Visibility.Visible;

                canvPlay.Visibility = Visibility.Hidden;
                canvGameMode.Visibility = Visibility.Hidden;

                g0.IsEnabled = true;
                g1.IsEnabled = true;
                g2.IsEnabled = true;
                g3.IsEnabled = true;
                g4.IsEnabled = true;
                g5.IsEnabled = true;
                g6.IsEnabled = true;
                g7.IsEnabled = true;
                g8.IsEnabled = true;

                return true;
            }
            return false;
        }

        // Move to main menu
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

        // Reset game state to initial
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
        }

        // Check for win or draw
        private bool checkForWin()
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

                g0.IsEnabled = false;
                g1.IsEnabled = false;
                g2.IsEnabled = false;
                g3.IsEnabled = false;
                g4.IsEnabled = false;
                g5.IsEnabled = false;
                g6.IsEnabled = false;
                g7.IsEnabled = false;
                g8.IsEnabled = false;

                MessageBox.Show(winMessage, "Results", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
        
        // Find Server COM port
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
        
        // Connect to Server
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
                    Application.Current.Shutdown();
                }
            }
        }

        // Send command to server and recieve response
        private string ExecCommand(string command)
        {
            if (serial != null)
            {
                try
                {
                    if(!serial.IsOpen)
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
            return "Serial port isnt initialized.";
        }
    } 
}