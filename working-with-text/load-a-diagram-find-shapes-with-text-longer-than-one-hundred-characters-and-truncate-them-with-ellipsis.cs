using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output file path (adds "_truncated" suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_truncated.vsdx");

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape
                    string currentText = shape.Text?.Value?.Text ?? string.Empty;

                    // Check if the text exceeds 100 characters
                    if (currentText.Length > 100)
                    {
                        // Truncate to 97 characters and add ellipsis (total 100)
                        string truncated = currentText.Substring(0, 97) + "...";

                        // Clear existing text runs
                        shape.Text.Value.Clear();

                        // Add the new truncated text as a Txt object
                        shape.Text.Value.Add(new Txt(truncated));
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}