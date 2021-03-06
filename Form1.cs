﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Programas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void executeProgram(string name)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\applications" + name);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] directory = Directory.GetFiles(Application.StartupPath + @"\applications");
            if(directory.Length > 0)
            {
                foreach (string dir in directory)
                {
                    int index = dir.LastIndexOf(@"\");
                    string prog = dir.Substring(index);
                    System.Diagnostics.Process.Start(Application.StartupPath + @"\applications" + prog).WaitForExit();
                }
                MessageBox.Show("Instalação completada!");
            }
            else
            {
                MessageBox.Show("Não há programas na página!");
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Application.StartupPath + @"\applications");
            if(files.Length == 0)
            {
                this.label2.Text = "Nenhum programa foi encontrado!";
                this.panel1.Visible = false;
                this.Anchor = 0;
            }
            else
            {
                this.label2.Visible = false;
                this.label3.Visible = false;
                this.button1.Visible = false;
                int top = -30;
                int left = 110;

                Button total = new Button();
                total.Location = new Point(125, 37);
                total.AutoSize = true;
                total.Text = "Instalação Total";
                total.Click += new EventHandler(this.button7_Click);
                this.Controls.Add(total);
                top += total.Height + 16;

                foreach (string file in files)
                {
                    int index = file.LastIndexOf(@"\");
                    string name = file.Substring(index);
                    int final = name.LastIndexOf(@".");
                    Button button = new Button();
                    button.Top = top;
                    button.Left = left;
                    button.Size = new Size(101, 23);
                    button.AutoSize = true;
                    button.Text = name.Substring(1, final - 1);
                    button.Click += (teste, i) => this.executeProgram(name);
                    this.panel1.Controls.Add(button);
                    //this.Controls.Add(button);
                    top += button.Height + 16;
                }

                Button exit = new Button();
                exit.Location = new Point(138, 387);
                exit.AutoSize = true;
                exit.Text = "Sair";
                exit.Click += new EventHandler((teste, i) => { Application.Exit(); });
                this.Controls.Add(exit);
                top += exit.Height + 16;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
