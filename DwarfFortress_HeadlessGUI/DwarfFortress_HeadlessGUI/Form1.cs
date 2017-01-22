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
        string workingFilename = @"Dwarf Fortress.exe";
        List<string> worldArray = new List<string>();
        string tempName;
        string worldGenFolder;
        string worldGenFile = "world_gen.txt";
        string configPath = @"config.txt";

        int worldInt;
        int runThis = 1;
        int genNumber = 1;
        int worldNumber;

        public Form1()
        {
            // Loading up the program.  Hopefully the config file bit works!
            InitializeComponent();
            seedType = textBox1.Text;
            genNumber = Convert.ToInt16(textBox3.Text);
            if (File.Exists(configPath))
            {
                FileStream CF = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader inConfig = new StreamReader(CF);
                string configLine = inConfig.ReadLine();
                workingFolder = @configLine;
                inConfig.Close();
                tempName = workingFolder + "\\data\\save\\";
                int directoryCount = System.IO.Directory.GetDirectories(@tempName).Length;
                textBox2.Text = Convert.ToString(directoryCount);
                workingFilename = workingFolder + "\\Dwarf Fortress.exe";
                worldGenFolder = workingFolder + "\\data\\init\\";
                FileStream fs = new FileStream(@worldGenFolder + worldGenFile, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fs);
                string line = reader.ReadLine();
                string cup = reader.ReadToEnd();
                fs.Position = 0;
                line = null;
                int loopCount = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("TITLE"))
                    {
                        line = line.Replace("	[TITLE:", "");
                        line = line.Replace("]", "");
                        worldArray.Add(line);
                    }
                    loopCount++;
                }
                comboBox1.DataSource = worldArray;
                reader.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int loopNum = genNumber;
            if (genNumber > 1)
            {
                seedType = "RANDOM";
                textBox1.Text = seedType;
            }
            while (loopNum > 0)
            {
                #region /* Running Dwarf Fortress with the chosen options */
                if (runThis >= 1)
                    #region /* To prevent an overflow, the following section used is */
                    try
                    {
                        Process headless = new Process();
                        headless.StartInfo.UseShellExecute = false;
                        headless.StartInfo.WorkingDirectory = @workingFolder;
                        headless.StartInfo.FileName = @workingFilename;
                        string tempArgs = @" -gen " + @worldNumber + @" " + @seedType + " \"" + @worldName + "\"";
                        headless.StartInfo.Arguments = @" -gen " + @worldNumber + @" " + @seedType + " \"" + @worldName + "\""; // if you need some
                        headless.StartInfo.CreateNoWindow = true;
                        //headless.EnableRaisingEvents = true;
                        //MessageBox.Show(tempArgs);
                        headless.Start();
                        headless.WaitForExit();
                        worldNumber = worldNumber + 1;
                        textBox2.Text = Convert.ToString(worldNumber);
                        FileStream CF = new FileStream(configPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        StreamWriter outConfig = new StreamWriter(CF);
                        outConfig.WriteLine(@workingFolder);
                        outConfig.Close();
                    }
                    finally
                    {
                    }
                    #endregion
                #region /* Should never end up here, but just in case */
                else
                {
                    Console.WriteLine("\nNothing was Passed as an Arguement!");
                }
                #endregion
                textBox2.Text = Convert.ToString(worldNumber);
                loopNum--;
                #endregion
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            seedType = textBox1.Text;  // Grabbing the seed if anything is ever entered and RANDOM is changed
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
            worldGenFolder = workingFolder + "\\data\\init\\";
            FileStream fs = new FileStream(@worldGenFolder + worldGenFile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string line = reader.ReadLine();
            string cup = reader.ReadToEnd();
            fs.Position = 0;
            line = null;
            int loopCount = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("TITLE"))
                {
                    line = line.Replace("	[TITLE:", "");
                    line = line.Replace("]", "");
                    worldArray.Add(line);
                }
                loopCount++;
            }
            comboBox1.DataSource = worldArray;
            reader.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // This is the Region Number that the program fills in automatically.  
            // It can be changed as the user wants, but there is a higher likelihood that the program will crash
            worldNumber = Convert.ToInt16(textBox2.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            worldName = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[20];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                seedType = finalString;
                textBox1.Text = seedType;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            genNumber = Convert.ToInt16(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
