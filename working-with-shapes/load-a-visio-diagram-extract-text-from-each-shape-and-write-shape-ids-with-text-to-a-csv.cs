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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Ensure the output directory exists.
        try
        {
            string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block and handle errors.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Open the CSV file for writing inside a using block.
                try
                {
                    using (StreamWriter writer = new StreamWriter(outputPath, false))
                    {
                        // Write CSV header.
                        writer.WriteLine("ShapeId,Text");

                        // Iterate through all pages.
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page.
                            foreach (Shape shape in page.Shapes)
                            {
                                // Retrieve plain text from the shape.
                                string text = string.Empty;
                                if (shape.Text != null && shape.Text.Value != null)
                                {
                                    text = shape.Text.Value.Text ?? string.Empty;
                                }

                                // Skip shapes with no text.
                                if (string.IsNullOrWhiteSpace(text))
                                    continue;

                                // Escape double quotes for CSV compliance.
                                string escapedText = text.Replace("\"", "\"\"");

                                // Write the shape ID and escaped text as a CSV line.
                                writer.WriteLine($"{shape.ID},\"{escapedText}\"");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error writing CSV file: {ex.Message}");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading Visio diagram: {ex.Message}");
            return;
        }

        Console.WriteLine($"Successfully exported shape texts to \"{outputPath}\".");
    }
}