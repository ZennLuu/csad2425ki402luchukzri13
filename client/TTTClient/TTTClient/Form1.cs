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

            // For now: requires manual COM-port selection
            SerialPort serial = new SerialPort("COM10", 115200);

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
    }
}