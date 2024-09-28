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
    }
}