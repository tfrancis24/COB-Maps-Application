using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

          //Selecting a csv/input file
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

          private void cancelButton_Click(object sender, EventArgs e)
          {
               //Add if else or dialog box asking users confirmation to close app later
               Application.Exit();
          }

          private void fileNameTextBox_TextChanged(object sender, EventArgs e)
          {

          }

          private void mapButton_Click(object sender, EventArgs e)
          {
               //Add code for ReadCSV later...
               //ReadCSV();

               //Saves file "MapTest" to the user's desktop
               string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
               StreamWriter sw = new StreamWriter(desktopPath + "\\MapTest.html");
               //{
               //     //What does this do
               //     AutoFlush = true
               //};

          }

          public void Head(StreamWriter writer)
          {
               //StreamWriter _writer = new StreamWriter("test.html");
               List<string> html = new List<string>
                 {
                     "<!DOCTYPE html>",
                     "<html>",
                     "<head>",
                     "  <meta http-equiv=\"content - type\" content=\"text/html; charset = UTF-8\" /> ",
                     "  <title>Google Maps Multiple Markers</title>",
                     "    <style>",
                     "      html, body, #map {",
                     "        height: 100%;",
                     "        margin: 0px;",
                     "        padding: 0px",
                     "      }",
                     "    </style>",
                     "  <script src=\"http://maps.google.com/maps/api/js?sensor=false\" ",
                     "          type=\"text/javascript\"></script>",
                     "</head>",
                     "<body>",
                     "  <div id=\"map\"></div>",
                     "",
                     "  <script type=\"text/javascript\">",
                     "    var locations = ["
                 };

               html.ForEach(writer.WriteLine);
          }

          public void Tail(StreamWriter writer)
          {
               List<string> html = new List<string>
                 {
                     "    ];",
                     " ",
                     "    var map = new google.maps.Map(document.getElementById('map'), {",
                     "      zoom: 10,",
                     "      center: new google.maps.LatLng(45.527367, -122.660251),",
                     "      mapTypeId: google.maps.MapTypeId.ROADMAP",
                     "    });",
                     "",
                     "    var infowindow = new google.maps.InfoWindow();",
                     "",
                     "    var marker, i;",
                     "",
                     "    for (i = 0; i < locations.length; i++) { ",
                     "      marker = new google.maps.Marker({",
                     "        position: new google.maps.LatLng(locations[i][1], locations[i][2]),",
                     "        map: map",
                     "      });",
                     "      google.maps.event.addListener(marker, 'click', (function(marker, i) {",
                     "        return function() {",
                     "          infowindow.setContent(locations[i][0]);",
                     "          infowindow.open(map, marker);",
                     "        }",
                     "      })(marker, i));",
                     "    }",
                     "  </script>",
                     "</body>",
                     "</html>"
                 };

               html.ForEach(writer.WriteLine);
          }
     }
}
