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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("ShapeId,Text");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text from the shape.
                        string text = shape.Text?.Value?.Text ?? string.Empty;

                        // Skip shapes without text.
                        if (string.IsNullOrWhiteSpace(text))
                            continue;

                        // Escape double quotes in text for CSV compliance.
                        string escapedText = text.Replace("\"", "\"\"");

                        // Write shape ID and text to CSV.
                        writer.WriteLine($"{shape.ID},\"{escapedText}\"");
                    }
                }
            }

            Console.WriteLine($"Successfully exported shape texts to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}