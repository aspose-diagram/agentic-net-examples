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
        public static void Main()
        {
            try
            {

                // Path to the Visio file
                string visioPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

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
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(solutionXmlList, jsonOptions);

                // Write JSON to file
                string outputPath = "solutionxml.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {solutionXmlList.Count} SolutionXML entries to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}