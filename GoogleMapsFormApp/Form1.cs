using Geocode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GoogleMapsFormApp
{
    public partial class Form1 : Form
     {
          //Geocode object - place the API key here
          public GeocodeClient geocodeClient = new GeocodeClient("");

          //Holds a string for the companies name, and MapLocation object
          public List<Tuple<string, MapLocation>> locationData = new List<Tuple<string, MapLocation>>();

          //Holds path of the location input file. For testing only
          public string inputPath = "Put PATH FILE here";

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
               readCSV();

               //Saves file "MapTest" to the user's desktop
               string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
               StreamWriter sw = new StreamWriter(desktopPath + "\\MapTest.html")
               {
                   //This fixed the issue of  streamwriter randomly freezing the write file process
                   //Flushing the output buffer to keep it from being full
                   AutoFlush = true
               };

               //These three functions write parts of the HTML file
               Head(sw);
               writeLocations(sw, locationData);
               Tail(sw);

               //Prints after above methods run, indicating the file has been created
               MessageBox.Show("Created File");

          }

          //Reads location data from an input CSV file 
          public void readCSV()
          {
               //Loops through lines in csv file, skip the header row 
               foreach (var line in File.ReadAllLines(inputPath, Encoding.GetEncoding(1250)).Skip(1))
               {
                    //Each rows address data gets split into the array seperated by a comma
                    string[] addressInfo = line.Split(',');

                    //MapLocation object is intialized with values from array
                    MapLocation location = geocodeClient.GetMapLocation(new Address
                    {
                         Street = addressInfo[1],
                         Apt = addressInfo[2], //null
                         City = addressInfo[3],
                         Region = addressInfo[4],
                         PostalCode = addressInfo[5]
                    });
                      
                    //Adds the company name and the maplocation tied to it into the list
                    locationData.Add(new Tuple<string, MapLocation>(addressInfo[0], location));

               }      
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
                     "  <title>Where Do They Come From?</title>",
                     "    <style>",
                     "      html, body, #map {",
                     "        height: 100%;",
                     "        margin: 0px;",
                     "        padding: 0px",
                     "      }",
                     "    </style>",
                     "  <script src=\"http://maps.google.com/maps/api/js?sensor=false\" ", //use google maps API?
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

          public void writeLocations(StreamWriter writer, List<Tuple<string, MapLocation>> locationData)
          {
                for (int i = 0; i < locationData.Count; i++)
                {
                    if (i == locationData.Count - 1)
                    {
                        //last element, no ending comma
                        writer.Write($"['{locationData[i].Item1}', {locationData[i].Item2.latitude}, {locationData[i].Item2.longitude}]\n");
                    }
                    else
                    {
                        writer.Write($"['{locationData[i].Item1}', {locationData[i].Item2.latitude}, {locationData[i].Item2.longitude}],\n");
                    }
                }
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
