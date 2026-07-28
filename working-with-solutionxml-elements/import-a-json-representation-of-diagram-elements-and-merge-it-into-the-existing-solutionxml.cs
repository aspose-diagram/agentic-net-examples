using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class DiagramMerger
{
    static void Main()
    {
        try
        {

            // Paths for the existing diagram, JSON input and the output diagram
            string existingDiagramPath = "ExistingDiagram.vsdx";
            string jsonInputPath = "Elements.json";
            string outputDiagramPath = "MergedDiagram.vsdx";

            // Load the existing Visio diagram using the constructor that accepts a file name
            Diagram mainDiagram = new Diagram(existingDiagramPath);

            // Read and parse the JSON representation of diagram elements
            string jsonContent = File.ReadAllText(jsonInputPath);
            using JsonDocument jsonDoc = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDoc.RootElement;

            // Create a secondary (temporary) diagram that will hold the elements from JSON
            Diagram secondaryDiagram = new Diagram(); // default constructor creates an empty diagram

            // --------------------------------------------------------------------
            // Add shapes defined in the JSON to the secondary diagram
            // Expected JSON format:
            // {
            //   "shapes": [
            //     { "masterName": "Rectangle", "pinX": 2.0, "pinY": 3.0, "pageIndex": 0 },
            //     ...
            //   ],
            //   "solutionXml": { "name": "MyData", "xmlValue": "<data>...</data>" }
            // }
            // --------------------------------------------------------------------
            if (root.TryGetProperty("shapes", out JsonElement shapesElement) &&
                shapesElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement shape in shapesElement.EnumerateArray())
                {
                    string masterName = shape.GetProperty("masterName").GetString();
                    double pinX = shape.GetProperty("pinX").GetDouble();
                    double pinY = shape.GetProperty("pinY").GetDouble();
                    int pageIndex = shape.GetProperty("pageIndex").GetInt32();

                    // AddShape(double pinX, double pinY, string masterName, int pageIndex)
                    secondaryDiagram.AddShape(pinX, pinY, masterName, pageIndex);
                }
            }

            // --------------------------------------------------------------------
            // Merge SolutionXML data from JSON into the main diagram (if present)
            // --------------------------------------------------------------------
            if (root.TryGetProperty("solutionXml", out JsonElement solXmlElement))
            {
                string name = solXmlElement.GetProperty("name").GetString();
                string xmlValue = solXmlElement.GetProperty("xmlValue").GetString();

                // Create a SolutionXML instance and add it to the collection
                SolutionXML solutionXml = new SolutionXML(name, xmlValue);
                mainDiagram.SolutionXMLs.Add(solutionXml);
            }

            // Combine the secondary diagram into the main diagram
            mainDiagram.Combine(secondaryDiagram);

            // Save the merged diagram to a new file using the Save(string, SaveFileFormat) method
            mainDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            // Clean up resources
            mainDiagram.Dispose();
            secondaryDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
