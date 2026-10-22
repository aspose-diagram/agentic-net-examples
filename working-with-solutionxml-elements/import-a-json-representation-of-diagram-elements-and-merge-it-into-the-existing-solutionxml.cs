using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Represents a single element from the JSON file that will be stored in SolutionXML.
    private class DiagramElement
    {
        public string Name { get; set; }
        public string XmlValue { get; set; }
    }

    static void Main(string[] args)
    {
        // Expected arguments: <diagramPath> <jsonPath> <outputPath>
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string jsonPath = args.Length > 1 ? args[1] : "elements.json";
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard: ensure the source Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the JSON file exists.
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        // Read and deserialize the JSON content.
        List<DiagramElement> elements;
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            elements = JsonSerializer.Deserialize<List<DiagramElement>>(jsonContent);
            if (elements == null)
            {
                Console.Error.WriteLine("Failed to deserialize JSON: result is null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or parsing JSON file: {ex.Message}");
            return;
        }

        // Load the existing Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each element from JSON and add it to the diagram's SolutionXML collection.
                foreach (DiagramElement elem in elements)
                {
                    // Create a new SolutionXML instance and set its properties.
                    SolutionXML solXml = new SolutionXML();
                    solXml.Name = elem.Name ?? string.Empty;
                    solXml.XmlValue = elem.XmlValue ?? string.Empty;

                    // Add the new SolutionXML element to the diagram.
                    diagram.SolutionXMLs.Add(solXml);
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}