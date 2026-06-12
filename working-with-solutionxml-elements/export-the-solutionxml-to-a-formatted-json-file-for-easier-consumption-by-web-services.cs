using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramSolutionXmlExport
{
    // DTO for JSON serialization
    public class SolutionXmlDto
    {
        public string Name { get; set; } = null!;
        public string XmlValue { get; set; } = null!;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Input Visio file path (adjust as needed)
            string visioPath = "input.vsdx";

            // Output JSON file path
            string jsonOutputPath = "solutionxml.json";

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Collect SolutionXML entries
            List<SolutionXmlDto> solutionXmlList = new List<SolutionXmlDto>();
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                solutionXmlList.Add(new SolutionXmlDto
                {
                    Name = solutionXml.Name,
                    XmlValue = solutionXml.XmlValue
                });
            }

            // Serialize to formatted JSON
            string json = JsonSerializer.Serialize(solutionXmlList, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Write JSON to file
            try
            {
                File.WriteAllText(jsonOutputPath, json);
                Console.WriteLine($"SolutionXML exported successfully to '{jsonOutputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write JSON file: {ex.Message}");
            }
        }
    }
}