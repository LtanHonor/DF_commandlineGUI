using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DwarfFortress_HeadlessGUI
{
    public partial class Form1 : Form
    {
        string seedType;
        string worldName;
        string worldSeed;
        string workingFolder;
        string workingFilename;
        string worldNumber;
        string worldArray;
        string tempName;

        int worldInt;
        int runThis = 1;
        //worldNumber = @"20";

        public Form1()
        {
            InitializeComponent();
            //System.IO.DirectoryInfo
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            #region /* Grabbing Command Line Arguments */
            if (runThis >= 1)
                #region /* To prevent an overflow, the following section used is */
                try
                {
                    //worldNumber = int.Parse(args[0]);
                    //worldInt = int.Parse(args[1]);

                    #region /* Listing World Options */
                    //if (worldInt <= 0)
                    //    worldName = "Dereth";
                    //else if (worldInt == 1)
                    //    worldName = "LARGE ISLAND";
                    //else if (worldInt == 2)
                    //    worldName = "LARGE REGION";
                    //else if (worldInt == 3)
                    //    worldName = "MEDIUM ISLAND";
                    //else if (worldInt == 4)
                    //    worldName = "MEDIUM REGION";
                    //else if (worldInt == 5)
                    //    worldName = "SMALL ISLAND";
                    //else if (worldInt == 6)
                    //    worldName = "SMALL REGION";
                    //else if (worldInt == 7)
                    //    worldName = "SMALLER ISLAND";
                    //else if (worldInt == 8)
                    //    worldName = "SMALLER REGION";
                    //else if (worldInt == 9)
                    //    worldName = "POCKET ISLAND";
                    //else if (worldInt == 10)
                    //    worldName = "POCKET REGION";
                    //else if (worldInt == 11)
                    //    worldName = "World001Region";
                    //else if (worldInt == 12)
                    //    worldName = "DAS FANTASY LAND";
                    //else if (worldInt == 13)
                    //    worldName = "Ltan Fantasy";
                    //else if (worldInt == 14)
                    //    worldName = "Dereth";
                    //else if (worldInt == 15)
                    //    worldName = "MEDIUM2";
                    //else if (worldInt == 16)
                    //    worldName = "Sinister Request";
                    //else if (worldInt == 17)
                    //    worldName = "Reworking A Default";
                    #endregion
                    Process headless = new Process();
                    headless.StartInfo.UseShellExecute = false;
                    headless.StartInfo.WorkingDirectory = @workingFolder;
                    headless.StartInfo.FileName = @workingFilename;
                    headless.StartInfo.Arguments = @" -gen " + worldNumber + " " + seedType + " " + worldName; // if you need some
                    headless.StartInfo.CreateNoWindow = true;
                    //headless.EnableRaisingEvents = true;

                    headless.Start();
                    headless.WaitForExit();
                }
                finally
                {
                }
                #endregion
            #region /*  */
            else
            {
                Console.WriteLine("\nNothing was Passed as an Arguement!");
            }
            #endregion

            #endregion
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            seedType = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog2.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
            workingFilename = openFileDialog2.FileName;

        }


        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                workingFolder = folderBrowserDialog1.SelectedPath;
                tempName = workingFolder + "\\data\\save\\";
                int directoryCount = System.IO.Directory.GetDirectories(@tempName).Length;
                textBox2.Text = Convert.ToString(directoryCount);
            }
            workingFilename = workingFolder + "\\Dwarf Fortress.exe";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            worldNumber = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            worldName = textBox3.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    System.IO.StreamReader sr = new
            //       System.IO.StreamReader(openFileDialog3.FileName);
            //    MessageBox.Show(sr.ReadToEnd());
            //    sr.Close();
            //}
                    //the path of the file
        FileStream inFile = new FileStream(openFileDialog3.FileName, FileMode.Open, FileAccess.Read);
        StreamReader reader = new StreamReader(inFile);
        string record;
        string input;
        input = "TITLE:";
        try
        {
            //the program reads the record and displays it on the screen
            record = reader.ReadLine();
            while (record != null)
            {
                if (record.Contains(input))
                {
                    worldArray = record;
                }
                    record = reader.ReadLine();
            }
        }
        finally
        {
            //after the record is done being read, the progam closes
            reader.Close();
            inFile.Close();
        }
        Console.ReadLine();
    
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                string tempName = folderBrowserDialog2.SelectedPath;
                int directoryCount = System.IO.Directory.GetDirectories(@tempName).Length;
                textBox2.Text = Convert.ToString(directoryCount);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
