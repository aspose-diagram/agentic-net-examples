using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output path (original name with suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (assumes the circle shape has ID 1)
            // Adjust the ID as needed for your specific diagram.
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape is not deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("The specified shape is marked as deleted.");
                return;
            }

            // Set fill pattern to solid (value 1) to make fill visible
            shape.Fill.FillPattern.Value = 1;

            // Set fill foreground color to blue using hex string
            shape.Fill.FillForegnd.Value = "#0000FF";

            // Set line weight to 0.5 points (0.5 / 72 inches)
            shape.Line.LineWeight.Value = 0.5 / 72.0;

            // Save the modified diagram to a new file in VSDX format
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