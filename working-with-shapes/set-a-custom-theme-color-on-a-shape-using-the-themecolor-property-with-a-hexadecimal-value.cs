using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example ID = 1)
            Shape shape = page.Shapes.GetShape(1);

            // Set a custom fill color on the shape using a hexadecimal value
            // The FillForegnd cell holds the foreground fill color; assign via .Value
            shape.Fill.FillForegnd.Value = "#FF5733";

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}