using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using System.Reflection.Emit;
using System.IO;

namespace TTTClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Fill comboBox1 on initialization
            fill_comboBox1();
        }

        // Send and recieve message if COM-Port selected
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set up communication speed same as server
            const int baud_rate = 115200;

            // Create chosen serial port object
            SerialPort serial = new SerialPort(comboBox1.Text, baud_rate);

            try
            {
                // Open the serial port
                serial.Open();
                label1.Text = "Serial port opened.";

                // Send message to ESP32
                string message = "Hello from client!";
                serial.WriteLine(message);
                Thread.Sleep(1000);
                label2.Text = $"Sent to ESP32: {message}";

                // Read response from ESP32
                string response = serial.ReadLine();
                label3.Text = $"Received from ESP32: {response}";
            }
            catch (Exception ex)
            {
                // Output error if occurred
                label4.Text = $"Error: {ex.Message}";
            }
            finally
            {
                // Close the serial port
                if (serial.IsOpen)
                {
                    serial.Close();
                    label4.Text = "Serial port closed.";
                }
            }
        }

        // Fill comboBox1 with available COM-Ports
        private void fill_comboBox1()
        {       
            // Make list of available COM-Ports
            string[] available_ports = SerialPort.GetPortNames();

            // Fill comboBox1
            comboBox1.Items.Clear();
            for (int i = 0; i < available_ports.Length; i++)
            {
                comboBox1.Items.Add(available_ports[i]);
            }
        }
        private bool cc = false;
        private void place_cross(object sender)
        {
            try
            {
                string projectDir = AppDomain.CurrentDomain.BaseDirectory;

                // Combine the base directory with the relative path to "media" folder
                string mediaDir = Path.Combine(Directory.GetParent(projectDir).Parent.Parent.FullName, "media");

                PictureBox pictureBox = (PictureBox)sender;
                Image xImage = Image.FromFile(Path.Combine(mediaDir, "cross.png"));
                Image oImage = Image.FromFile(Path.Combine(mediaDir, "circle.png"));
                pictureBox.Image = cc ? xImage : oImage;
                cc = !cc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void tt0_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt1_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt2_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt3_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }


        private void tt4_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt5_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt6_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt7_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }

        private void tt8_Click(object sender, EventArgs e)
        {
            place_cross(sender);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}