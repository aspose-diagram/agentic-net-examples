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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first shape on the first page.
            Shape shape = diagram.Pages[0].Shapes[0];
            if (shape == null)
            {
                Console.Error.WriteLine("No shape found on the first page.");
                return;
            }

            // Enable gradient fill if not already enabled.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set the fill pattern to a gradient (value 25 corresponds to gradient fill).
            shape.Fill.FillPattern.Value = 25;

            // Set the gradient angle to 0 degrees for a horizontal fill.
            shape.Fill.GradientFill.GradientAngle.Value = 0;

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}