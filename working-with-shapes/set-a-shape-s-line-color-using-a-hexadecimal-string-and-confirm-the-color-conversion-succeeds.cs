using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the first page (index 0)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example uses ID = 1)
                // Adjust the shape ID as needed for your diagram
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                    return;
                }

                // Define the desired line color as a hexadecimal string
                string hexColor = "#FF0000"; // Red

                // Set the shape's line color
                shape.Line.LineColor.Value = hexColor;

                // Verify that the color was set correctly
                string actualColor = shape.Line.LineColor.Value;
                if (string.Equals(hexColor, actualColor, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Line color set successfully to {actualColor}.");
                }
                else
                {
                    throw new Exception($"Line color verification failed. Expected: {hexColor}, Actual: {actualColor}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}