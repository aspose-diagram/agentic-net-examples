using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

        // Guard: ensure the input Visio file exists
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
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the triangle shape.
                // This example assumes the triangle has shape ID 1.
                // Adjust the ID as needed for your specific diagram.
                Shape triangle = page.Shapes.GetShape(1);

                // Apply default shadow effect using cell-based APIs
                // Set shadow type to simple drop shadow
                triangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                // Set default shadow color (black)
                triangle.Fill.ShdwForegnd.Value = "#000000";

                // Set default shadow transparency (30% transparent)
                triangle.Fill.ShdwForegndTrans.Value = 0.3;

                // Set default shadow offsets (0.1 inches horizontally and vertically)
                triangle.Fill.ShapeShdwOffsetX.Value = 0.1;
                triangle.Fill.ShapeShdwOffsetY.Value = 0.1;

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}