using System;
using System.IO;
using System.Collections.Generic;
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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect IDs of shapes whose NameU matches the pattern "Node_*".
                List<long> matchingIds = new List<long>();

                // Iterate over all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Guard against null names and check the prefix.
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.StartsWith("Node_"))
                        {
                            // Shape.ID is of type long.
                            matchingIds.Add(shape.ID);
                        }
                    }
                }

                // Write the collected IDs to a CSV file.
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Optional header.
                    writer.WriteLine("ShapeID");

                    foreach (long id in matchingIds)
                    {
                        writer.WriteLine(id);
                    }
                }

                Console.WriteLine($"Extracted {matchingIds.Count} shape IDs to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}