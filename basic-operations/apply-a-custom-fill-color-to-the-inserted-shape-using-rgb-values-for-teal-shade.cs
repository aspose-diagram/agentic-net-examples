using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output file path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (assumes at least one page exists)
            Page page = diagram.Pages[0];

            // Insert a rectangle shape at position (1,1) using the "Rectangle" master
            // AddShape returns the shape ID (long)
            long shapeId = page.AddShape(1.0, 1.0, "Rectangle");

            // Retrieve the newly added shape using its ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Set solid fill pattern
            shape.Fill.FillPattern.Value = 1; // 1 = solid

            // Apply teal fill color using hex representation of RGB(0,128,128)
            shape.Fill.FillForegnd.Value = "#008080";

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}