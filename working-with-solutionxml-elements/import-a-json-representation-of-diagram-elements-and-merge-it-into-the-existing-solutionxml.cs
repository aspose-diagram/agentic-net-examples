using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramMergeExample
{
    // Represents a single entry from the JSON input.
    public class SolutionXmlEntry
    {
        public string Name { get; set; }
        public string XmlValue { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // Path to the existing Visio diagram file.
                string diagramPath = @"C:\Diagrams\ExistingDiagram.vsdx";

                // Load the existing diagram using the constructor rule.
                Diagram diagram = new Diagram(diagramPath);

                // JSON string containing the solution XML entries to be merged.
                // Example:
                // [
                //   { "Name": "CustomData1", "XmlValue": "<data>Value1</data>" },
                //   { "Name": "CustomData2", "XmlValue": "<info>Value2</info>" }
                // ]
                string jsonInput = File.ReadAllText(@"C:\Input\solutionxml.json");

                // Deserialize the JSON into a list of entries.
                List<SolutionXmlEntry> entries = JsonSerializer.Deserialize<List<SolutionXmlEntry>>(jsonInput);

                // Merge each entry into the diagram's SolutionXMLs collection.
                foreach (var entry in entries)
                {
                    // Create a new SolutionXML object using the constructor rule.
                    SolutionXML solutionXml = new SolutionXML(entry.Name, entry.XmlValue);

                    // Add the new SolutionXML to the diagram.
                    // The SolutionXMLs collection supports an Add method.
                    diagram.SolutionXMLs.Add(solutionXml);
                }

                // Save the updated diagram back to file using the Save method rule.
                diagram.Save(diagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}