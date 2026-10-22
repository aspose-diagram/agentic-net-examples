using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (index 0). Adjust as needed.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Aspose.Diagram.Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes.");
                    return;
                }

                // Get the first shape (ID 1 is typical, but we use the first in collection).
                Aspose.Diagram.Shape shape = page.Shapes[0];

                // Define the desired line color as a hexadecimal string.
                string hexColor = "#FF0000";

                // Set the shape's line color.
                shape.Line.LineColor.Value = hexColor;

                // Verify that the color was set correctly.
                string actualColor = shape.Line.LineColor.Value;
                if (!string.Equals(actualColor, hexColor, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Line color verification failed. Expected: {hexColor}, Actual: {actualColor}");
                }

                Console.WriteLine($"Line color successfully set to {actualColor}.");

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}