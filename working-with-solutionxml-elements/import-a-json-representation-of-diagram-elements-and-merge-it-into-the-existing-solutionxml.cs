using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramSolutionXmlMerge
{
    // DTO matching the JSON structure
    public class SolutionXmlItem
    {
        public string Name { get; set; } = null!;
        public string XmlValue { get; set; } = null!;
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string jsonPath = "solutionElements.json";
                string outputPath = "merged_output.vsdx";

                // Load existing Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read and deserialize JSON file
                if (!File.Exists(jsonPath))
                {
                    Console.WriteLine($"JSON file not found: {jsonPath}");
                    return;
                }

                string jsonContent = File.ReadAllText(jsonPath);
                List<SolutionXmlItem>? items = JsonSerializer.Deserialize<List<SolutionXmlItem>>(jsonContent);
                if (items == null)
                {
                    Console.WriteLine("Failed to parse JSON content.");
                    return;
                }

                // Merge each JSON element into the diagram's SolutionXML collection
                foreach (var item in items)
                {
                    bool found = false;
                    foreach (SolutionXML existing in diagram.SolutionXMLs)
                    {
                        if (existing.Name == item.Name)
                        {
                            // Update existing entry
                            existing.XmlValue = item.XmlValue;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        // Add new SolutionXML element
                        SolutionXML newSol = new SolutionXML();
                        newSol.Name = item.Name;
                        newSol.XmlValue = item.XmlValue;
                        diagram.SolutionXMLs.Add(newSol);
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}