using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (or create one if the diagram has no pages).
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Adding a new empty page.
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Insert a new rectangle shape at position (2,2) using the built‑in "Rectangle" master.
                // The fourth parameter (isCalculate) must be a bool.
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape object for further modifications.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has a solid fill pattern (1 = solid).
                shape.Fill.FillPattern.Value = 1;

                // Apply the teal color (RGB 0,128,128) using a hex string.
                shape.Fill.FillForegnd.Value = "#008080";

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}