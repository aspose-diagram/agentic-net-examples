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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Get the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: First page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Enable gradient fill if not already enabled.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set the gradient angle to 0 degrees for a horizontal fill.
            shape.Fill.GradientFill.GradientAngle.Value = 0;

            // Optionally, you can set a solid fill pattern to ensure visibility.
            shape.Fill.FillPattern.Value = 1; // Solid fill.

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}