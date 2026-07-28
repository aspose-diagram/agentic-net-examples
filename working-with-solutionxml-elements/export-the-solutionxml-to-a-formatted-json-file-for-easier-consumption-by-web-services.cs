using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramSolutionXmlExport
{
    // Simple DTO to hold SolutionXML data for JSON serialization
    public class SolutionXmlDto
    {
        public string Name { get; set; }
        public string XmlValue { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Validate arguments: input VSD file and output JSON file
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramSolutionXmlExport <input.vsd> <output.json>");
                return;
            }

            string inputVsdPath = args[0];
            string outputJsonPath = args[1];

            // Load the Visio diagram (Aspose.Diagram constructor loads the file)
            Diagram diagram = new Diagram(inputVsdPath);

            // Prepare a list to hold all SolutionXML entries
            List<SolutionXmlDto> solutionXmlList = new List<SolutionXmlDto>();

            // Iterate through the SolutionXML collection
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                solutionXmlList.Add(new SolutionXmlDto
                {
                    Name = solXml.Name,
                    XmlValue = solXml.XmlValue
                });
            }

            // Configure JSON serializer for formatted (indented) output
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            // Serialize the list to JSON
            string jsonContent = JsonSerializer.Serialize(solutionXmlList, jsonOptions);

            // Write the JSON content to the specified file
            File.WriteAllText(outputJsonPath, jsonContent);

            Console.WriteLine($"SolutionXML data exported to JSON file: {outputJsonPath}");
        }
    }
}