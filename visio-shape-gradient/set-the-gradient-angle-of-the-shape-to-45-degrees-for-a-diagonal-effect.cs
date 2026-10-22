using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

            // Get the first page (or active page) of the diagram.
            Page page = diagram.ActivePage;

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no shapes to modify.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // Enable gradient fill by setting the fill pattern to gradient (value 25).
            shape.Fill.FillPattern.Value = 25;

            // Turn on the gradient fill.
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set the gradient angle to 45 degrees for a diagonal effect.
            shape.Fill.GradientFill.GradientAngle.Value = 45.0;

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}