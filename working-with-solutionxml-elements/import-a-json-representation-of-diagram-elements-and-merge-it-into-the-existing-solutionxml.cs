using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, JSON file and the output diagram
            string diagramPath = "input.vsdx";
            string jsonPath = "elements.json";
            string outputPath = "output.vsdx";

            // Load the existing diagram using the provided constructor
            Diagram diagram = new Diagram(diagramPath);

            // Read and deserialize the JSON representation of SolutionXML items
            string json = File.ReadAllText(jsonPath);
            List<SolutionItem> items = JsonSerializer.Deserialize<List<SolutionItem>>(json);

            if (items != null)
            {
                foreach (SolutionItem item in items)
                {
                    // Create a new SolutionXML instance (constructor is allowed)
                    SolutionXML newSolutionXml = new SolutionXML(item.Name, item.Xml);

                    // Check if an entry with the same name already exists
                    SolutionXML existing = FindSolutionXML(diagram, item.Name);
                    if (existing != null)
                    {
                        // Replace the existing entry (remove then add)
                        diagram.SolutionXMLs.Remove(existing);
                    }

                    // Add the new SolutionXML to the diagram's collection
                    diagram.SolutionXMLs.Add(newSolutionXml);
                }
            }

            // Save the merged diagram using the provided Save method
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to locate a SolutionXML by its name within the diagram
    static SolutionXML FindSolutionXML(Diagram diagram, string name)
    {
        foreach (SolutionXML sx in diagram.SolutionXMLs)
        {
            // Assuming SolutionXML exposes a Name property
            if (sx.Name == name)
                return sx;
        }
        return null;
    }

    // DTO matching the JSON structure
    class SolutionItem
    {
        public string Name { get; set; }
        public string Xml { get; set; }
    }
}