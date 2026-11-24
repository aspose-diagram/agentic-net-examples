using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Define the output path (original name with "_updated" suffix).
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_updated.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape.
                    string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check for empty or whitespace‑only text.
                    if (string.IsNullOrWhiteSpace(shapeText))
                    {
                        // Clear any existing text fragments.
                        shape.Text.Value.Clear();

                        // Add default placeholder text.
                        shape.Text.Value.Add(new Txt("Placeholder"));
                    }
                }
            }

            // Save the modified diagram to a new file.
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