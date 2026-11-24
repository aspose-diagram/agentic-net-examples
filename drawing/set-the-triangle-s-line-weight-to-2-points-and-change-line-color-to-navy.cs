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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

            // Get the first page (you can adjust the page selection as needed).
            Page page = diagram.Pages[0];

            // Retrieve the shape representing the triangle.
            // Here we assume the triangle is the first shape on the page.
            // Adjust the shape ID or selection logic according to your diagram.
            Shape triangle = page.Shapes.GetShape(1);

            // Ensure the shape is not deleted.
            if (triangle.Del == BOOL.True)
            {
                Console.Error.WriteLine("The specified shape is marked as deleted.");
                return;
            }

            // Set line weight to 2 points (2/72 inches).
            triangle.Line.LineWeight.Value = 2.0 / 72.0;

            // Set line color to navy (hex color #000080).
            triangle.Line.LineColor.Value = "#000080";

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}