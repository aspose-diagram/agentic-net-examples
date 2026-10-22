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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Counter for generating simple incremental IDs (optional).
            int counter = 1;

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // ShapeCollection implements IEnumerable, so we can use foreach.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check if the text is empty or whitespace.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Generate a unique identifier. Here we use a GUID combined with a counter.
                        string uniqueId = $"ID-{Guid.NewGuid():N}-{counter++}";

                        // Clear any existing text runs.
                        shape.Text.Value.Clear();

                        // Add the new text as a single Txt object.
                        shape.Text.Value.Add(new Txt(uniqueId));

                        Console.WriteLine($"Shape ID {shape.ID} on page {page.ID} populated with identifier: {uniqueId}");
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}