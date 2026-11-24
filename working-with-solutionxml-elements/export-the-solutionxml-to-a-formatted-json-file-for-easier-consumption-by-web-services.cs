using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // DTO class for JSON serialization of SolutionXML elements
    private class SolutionXmlDto
    {
        public string Name { get; set; }
        public string XmlValue { get; set; }
    }

    static void Main(string[] args)
    {
        // Expect exactly two arguments: input Visio file path and output JSON file path
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputJsonPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect all SolutionXML elements into a list of DTOs
                List<SolutionXmlDto> solutionXmlList = new List<SolutionXmlDto>();
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Skip null entries just in case
                    if (solutionXml == null) continue;

                    solutionXmlList.Add(new SolutionXmlDto
                    {
                        Name = solutionXml.Name,
                        XmlValue = solutionXml.XmlValue
                    });
                }

                // Configure JSON serialization options for formatted (indented) output
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                // Serialize the list to a JSON string
                string jsonContent = JsonSerializer.Serialize(solutionXmlList, jsonOptions);

                // Write the JSON content to the specified output file
                File.WriteAllText(outputPath, jsonContent);
                Console.WriteLine($"Successfully exported {solutionXmlList.Count} SolutionXML element(s) to JSON file: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}