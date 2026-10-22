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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string currentText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check if the text exceeds 100 characters.
                    if (currentText.Length > 100)
                    {
                        // Truncate to 100 characters and append ellipsis.
                        string truncated = currentText.Substring(0, 100) + "...";

                        // Clear existing text runs.
                        shape.Text.Value.Clear();

                        // Add the new truncated text.
                        shape.Text.Value.Add(new Txt(truncated));
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}