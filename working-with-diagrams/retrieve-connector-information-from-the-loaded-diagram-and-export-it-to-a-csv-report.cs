using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command-line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard against empty output path
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Output CSV path is empty.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV lines with header
            List<string> csvLines = new List<string>();
            csvLines.Add("PageName,FromShapeID,ToShapeID");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page name (fallback to empty string if null)
                string pageName = page.NameU ?? string.Empty;

                // Iterate through each connector (Connect) on the page
                foreach (Connect connect in page.Connects)
                {
                    // Build CSV line: page name, source shape ID, target shape ID
                    string line = $"{pageName},{connect.FromSheet},{connect.ToSheet}";
                    csvLines.Add(line);
                }
            }

            // Write all CSV lines to the specified output file
            File.WriteAllLines(outputPath, csvLines);
            Console.WriteLine($"Connector report saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}