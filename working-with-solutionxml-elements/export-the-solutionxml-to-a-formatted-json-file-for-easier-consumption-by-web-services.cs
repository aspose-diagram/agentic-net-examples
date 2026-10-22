using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputJsonFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect SolutionXML elements into a simple serializable structure.
                var solutionList = new List<object>();

                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    solutionList.Add(new
                    {
                        Name = solutionXml.Name,
                        XmlValue = solutionXml.XmlValue
                    });
                }

                // Serialize the collection to formatted JSON.
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(solutionList, jsonOptions);

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);
                Console.WriteLine($"SolutionXML exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}