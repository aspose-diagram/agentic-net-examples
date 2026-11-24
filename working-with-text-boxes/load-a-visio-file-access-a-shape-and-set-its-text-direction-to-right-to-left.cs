using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Access the first shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: Page contains no shapes.");
                return;
            }
            Shape shape = page.Shapes[0];

            // TODO: Set the text direction of the shape to right-to-left.
            // The appropriate API may be:
            // shape.TextBlock.TextDirection.Value = TextDirectionValue.RightToLeft;
            // If TextDirectionValue.RightToLeft is not available, use the closest supported value.

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}