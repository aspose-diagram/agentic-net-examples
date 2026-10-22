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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare CSV writer.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Write CSV header.
                    writer.WriteLine("PageIndex,ShapeID,Text");

                    int pageIndex = 0;

                    // Iterate over each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate over each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the plain text of the shape.
                            string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                            // Escape double quotes for CSV compliance.
                            string escapedText = shapeText.Replace("\"", "\"\"");

                            // Write a CSV line for the current shape.
                            writer.WriteLine($"{pageIndex},{shape.ID},\"{escapedText}\"");
                        }

                        pageIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}