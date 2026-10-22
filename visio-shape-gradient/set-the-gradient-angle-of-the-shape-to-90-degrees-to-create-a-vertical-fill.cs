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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: Page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Enable gradient fill for the shape.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set the gradient angle to 90 degrees (vertical fill).
            shape.Fill.GradientFill.GradientAngle.Value = 90.0;

            // Optionally, set a simple gradient pattern (foreground and background colors).
            // This ensures the gradient is visible.
            shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground.
            shape.Fill.FillBkgnd.Value = "#0000FF";   // Blue background.

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}