using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Obtain the first page; if none exist, create a new blank page
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                // Add a new Page instance to the diagram's Pages collection
                page = new Page();
                diagram.Pages.Add(page);
            }

            // Insert a rectangle shape at coordinates (2,2) using the built‑in "Rectangle" master
            // The AddShape overload returns the shape ID (long)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the Shape object from the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape uses a solid fill pattern (1 = solid)
            shape.Fill.FillPattern.Value = 1;

            // Apply the custom teal color (RGB 0,128,128) using a hex string
            shape.Fill.FillForegnd.Value = "#008080";

            // Save the modified diagram as VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}