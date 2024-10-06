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
using static System.Net.Mime.MediaTypeNames;

namespace TTTClient
{
    public partial class Form1 : Form
    {
        string projectDir, mediaDir;

        private bool cc = false;

        // Set up communication speed same as server
        const int baud_rate = 115200;

        // Create chosen serial port object
        SerialPort serial;

        PictureBox[] grid = new PictureBox[9];

        public Form1()
        {
            InitializeComponent();

            // Setup media folder
            projectDir = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the relative path to "media" folder
            mediaDir = Path.Combine(Directory.GetParent(projectDir).Parent.Parent.FullName, "media");

            // Setup icon
            this.Icon = new Icon(Path.Combine(mediaDir, "circle.ico"));

            // Setup grid
            Grid.Image = System.Drawing.Image.FromFile(Path.Combine(mediaDir, "grid.png"));

            FillGridWithPBoxes();

            // Fill comboBox1 on initialization
            fill_ComPortsBox();
        }

        // Send and recieve message if COM-Port selected
        private void ComPortsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serial = new SerialPort(comboBox1.Text, baud_rate);
            }
            catch (Exception ex)
            {
                label4.Text = $"Error: {ex.Message}";
            }
        }

        // Fill comboBox1 with available COM-Ports
        private void fill_ComPortsBox()
        {
            // Make list of available COM-Ports
            string[] availablePorts = SerialPort.GetPortNames();

            // Fill comboBox1
            comboBox1.Items.Clear();
            for (int i = 0; i < availablePorts.Length; i++)
            {
                comboBox1.Items.Add(availablePorts[i]);
            }
        }


        private void tt0_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_0");
        }

        private void tt1_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_1");
        }

        private void tt2_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_2");
        }

        private void tt3_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_3");
        }


        private void tt4_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_4");
        }

        private void tt5_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_5");
        }

        private void tt6_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_6");
        }

        private void tt7_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_7");
        }

        private void tt8_Click(object sender, EventArgs e)
        {
            ExecCommand("MW_8");
        }

        private void FillGridWithPBoxes()
        {
            grid[0] = tt0;
            grid[1] = tt1;
            grid[2] = tt2;
            grid[3] = tt3;
            grid[4] = tt4;
            grid[5] = tt5;
            grid[6] = tt6;
            grid[7] = tt7;
            grid[8] = tt8;
        }

        private void ExecCommand(string command)
        {
            try
            {
                serial.Open();

                serial.WriteLine(command);

                label3.Text = command;

                string recCommand = serial.ReadLine();

                label4.Text = recCommand;

                if (recCommand[0] == 'M' && recCommand[1] == 'A')
                {
                    int cell = recCommand[3] - '0';
                    System.Drawing.Image xImage = System.Drawing.Image.FromFile(Path.Combine(mediaDir, "cross.png"));
                    System.Drawing.Image oImage = System.Drawing.Image.FromFile(Path.Combine(mediaDir, "circle.png"));
                    grid[cell].Image = cc ? xImage : oImage;
                    cc = !cc;
                }

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }
            finally {
                if (serial.IsOpen)
                    serial.Close();
            }
        }
    }
}