using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare to write CSV output.
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header.
                    writer.WriteLine("Name,XmlValue");

                    // Iterate over all SolutionXML elements.
                    foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                    {
                        // Retrieve name and XML value.
                        string name = solutionXml.Name ?? string.Empty;
                        string xmlValue = solutionXml.XmlValue ?? string.Empty;

                        // Escape double quotes by doubling them and wrap fields in quotes.
                        string escapedName = $"\"{name.Replace("\"", "\"\"")}\"";
                        string escapedXml = $"\"{xmlValue.Replace("\"", "\"\"")}\"";

                        // Write CSV line.
                        writer.WriteLine($"{escapedName},{escapedXml}");
                    }
                }

                Console.WriteLine($"Successfully exported {diagram.SolutionXMLs.Count} SolutionXML entries to CSV.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}