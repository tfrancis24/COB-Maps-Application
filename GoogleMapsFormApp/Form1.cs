using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleMapsFormApp
{
     public partial class Form1 : Form
     {
          public Form1()
          {
               InitializeComponent();
          }

          private void Form1_Load(object sender, EventArgs e)
          {

          }

          private void mapButton_Click(object sender, EventArgs e)
          {

          }

          // Code for selecting a csv/input file
          private void inputButton_Click(object sender, EventArgs e)
          {
               OpenFile(fileNameTextBox);
          }

          //Dialog for opening a file
          private void OpenFile(TextBox t)
          {
               OpenFileDialog ofd = new OpenFileDialog();
               if (ofd.ShowDialog() == DialogResult.OK)
               {
                    try
                    {
                         System.IO.Stream myStream = null;
                         if ((myStream = ofd.OpenFile()) != null)
                         {
                              using (myStream)
                              {
                                   t.Text = ofd.FileName;
                              }
                         }
                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show("You must select a file path" + ex.Message, "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
               }
          }
     }
}
