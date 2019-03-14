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
          //Holds the API key
          public static string apiKey = "";

          //Geocode object - place the API key here
          public GeocodeClient geocodeClient = new GeocodeClient(apiKey);

          //Holds a string for the companies name, and MapLocation object
          public List<Tuple<string, MapLocation>> locationData = new List<Tuple<string, MapLocation>>();

          //Holds an int as a count variable and MapLocation object
          public List<Tuple<string, int, MapLocation>> locationDataCityCount = new List<Tuple<string, int, MapLocation>>();

          //Array of city count objects
          public CityCount[] cityCount;
       
          //Holds path of the location input file. For testing only
          public string inputPath = "";
          //public string systemPath = Path.Combine(Environment.CurrentDirectory, inputPath);


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
                         MessageBox.Show("You must select a .csv file path" + ex.Message, "Error",
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

               readCSVMarker();

               //Saves file "MapTest" to the user's desktop
               string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
               StreamWriter sw = new StreamWriter(desktopPath + "\\MapTest.html")
               {
                    //This fixed the issue of  streamwriter randomly freezing the write file process
                    //Flushing the output buffer to keep it from being full
                    AutoFlush = true
               };

               //These three functions write parts of the HTML file
               HeadMarker(sw);
               writeMarkerLocations(sw, locationData);
               TailMarker(sw);

               //Prints after above methods run, indicating the file has been created
               MessageBox.Show("Created File");

          }

          //When clicked, will map circles to a google map
          private void circleMapButton_Click(object sender, EventArgs e)
          {
               readCSVCircleCount();


               string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
               StreamWriter sw = new StreamWriter(desktopPath + "\\MapTest.html")
               {
                    //This fixed the issue of  streamwriter randomly freezing the write file process
                    //Flushing the output buffer to keep it from being full
                    AutoFlush = true
               };

               HeadCircle(sw);
               writeCircleLocations(sw, locationDataCityCount);
               TailCircle(sw);
               MessageBox.Show("Printed");


          }

          //Reads location data from an input CSV file
          //This method will be used to map pins
          public void readCSVMarker() //for marker map only
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
                         Apt = null, //null
                         City = addressInfo[3],
                         Region = addressInfo[4],
                         PostalCode = addressInfo[5]
                    });

                    //Adds the company name and the maplocation tied to it into the list
                    locationData.Add(new Tuple<string, MapLocation>(addressInfo[0], location));

               }
          }

          //Reads from the input file to count number of cities on the map
          public void readCSVCircleCount()
          {
               string[] lines = File.ReadAllLines(inputPath); //Reads all the lines of the file
               lines = lines.Skip(1).ToArray(); //Skips the header row

               int uniqueCityCount = 0; //Count of total unique cities in file
               cityCount = new CityCount[lines.Length]; //Array of CityCount objects, get the number of objects from the file length

               for (int i = 0; i < lines.Length; i++)
               {
                    string[] addressInfo = lines[i].Split(','); //Splits each row of the data into an array
                    string city = addressInfo[3];
                    string state = addressInfo[4];
                    bool found = false;

                    for (int j = 0; j < uniqueCityCount; j++)
                    {
                         if (cityCount[j].City == city && cityCount[j].State == state)
                         {
                              cityCount[j].Count++;
                              found = true;
                              break;
                         }
                    }

                    if (!found)
                    {
                         cityCount[uniqueCityCount] = new CityCount
                         {
                              City = city,
                              State = state,
                              Count = 1
                         };
                         uniqueCityCount++;
                    }
               }

               //Remove null indexes
               cityCount = cityCount.Where(c => c != null).ToArray();

               foreach (var cityCountObject in cityCount)
               {
                    //MapLocation object is intialized with values from array
                    MapLocation location = geocodeClient.GetMapLocation(new Address
                    {
                         Street = null,
                         Apt = null, //null
                         City = cityCountObject.City,
                         Region = cityCountObject.State,
                         PostalCode = null

                    });

                    //Adds the company name and the maplocation tied to it into the list
                    locationDataCityCount.Add(new Tuple<string, int, MapLocation>(cityCountObject.City + "," +
                        cityCountObject.State, cityCountObject.Count, location));
               }


          }


          public void HeadMarker(StreamWriter writer)
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
                     "  <script src=\"http://maps.google.com/maps/api/js?key=" + apiKey + "&callback=initMap\"",
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

          public void HeadCircle(StreamWriter writer)
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
                     "  <script src=\"http://maps.google.com/maps/api/js?key=" + apiKey + "&callback=initMap\"",
                     "          type=\"text/javascript\"></script>",
                     "</head>",
                     "<body>",
                     "  <div id=\"map\"></div>",
                     "",
                     "  <script type=\"text/javascript\">",
                     "var citymap = {};"
               };

               html.ForEach(writer.WriteLine);
          }

          public void writeMarkerLocations(StreamWriter writer, List<Tuple<string, MapLocation>> locationData)
          {
               for (int i = 0; i < locationData.Count; i++)
               {
                    if (i == locationData.Count - 1)
                    {
                         //last element, no ending comma
                         writer.Write($"		['{locationData[i].Item1}', {locationData[i].Item2.latitude}, {locationData[i].Item2.longitude}]\n");
                    }
                    else
                    {
                         writer.Write($"		['{locationData[i].Item1}', {locationData[i].Item2.latitude}, {locationData[i].Item2.longitude}],\n");
                    }
               }
          }

          public void writeCircleLocations(StreamWriter writer, List<Tuple<string, int, MapLocation>> locationData)
          {
               for (int i = 0; i < locationData.Count; i++)
               {
                    writer.Write("	citymap['" + locationData[i].Item3.City + "," + locationData[i].Item3.Region + "']= {\n"
                        + "		center: new google.maps.LatLng(" + locationData[i].Item3.latitude + ", " + locationData[i].Item3.longitude + "),\n"
                        + "		population: " + locationData[i].Item2 + ",\n"
                        + "		name:' " + locationData[i].Item3.City + ", " + locationData[i].Item3.Region + "'\n"
                        + "	};\n");
               }
          }

          public void TailMarker(StreamWriter writer)
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
                     "          position: new google.maps.LatLng(locations[i][1], locations[i][2]),",
                     "          map: map",
                     "          });",
                     
                     /* The below 4 lines make the infowindow open then the map is viewed*/
                     "	    var infowindow = new google.maps.InfoWindow({",
                     "		    content:locations[i][0]",
                     "		    });",
                     "	    infowindow.open(map, marker); ",
                     "",

                     "      google.maps.event.addListener(marker, 'mouseover', (function(marker, i) {",
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

          public void TailCircle(StreamWriter writer)
          {
               List<string> html = new List<string>
               {
                     "",
                     "",
                     "var cityCircle;",
                     "",
                     "function initialize() {",
                     "  var mapOptions = {",
                     "    zoom: 4,",
                     "    center: new google.maps.LatLng(37.09024, -95.712891),",
                     "    mapTypeId: google.maps.MapTypeId.ROADMAP",
                     "  };",
                     "",
                     "  var map = new google.maps.Map(document.getElementById('map'),",
                     "      mapOptions);",
                     "",
                     "  for (var city in citymap) {",
                     "    var populationOptions = {",
                     "      map: map,",
                     "      position: citymap[city].center,",
                     "      icon: {",
                     "      path: google.maps.SymbolPath.CIRCLE,",
                     "      scale: Math.sqrt(citymap[city].population)*2,",
                     "      strokeColor: '#FF0000',",
                     "      strokeOpacity: 0.8,",
                     "      strokeWeight: 2,",
                     "      fillColor: '#FF0000',",
                     "      fillOpacity: 0.35",
                     "      },",
                     "      clickable: true,",
                     "    };",
                     "    cityCircle = new google.maps.Marker(populationOptions);",
                     "  }",
                     "}",
                     "google.maps.event.addDomListener(window, 'load', initialize);",
                     "</script>",
                     "</body>",
                     "</html>"
                };

               html.ForEach(writer.WriteLine);
          }


     }
}
