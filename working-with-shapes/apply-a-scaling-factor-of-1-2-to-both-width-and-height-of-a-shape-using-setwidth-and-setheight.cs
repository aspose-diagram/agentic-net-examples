using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page.
                Page page = diagram.Pages[0];

                // Identify the shape to resize. Here we use shape ID 1 as an example.
                long shapeId = 1;

                // Retrieve the shape using the required cast to int.
                Shape shape = page.Shapes.GetShape((int)shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                    return;
                }

                // Read current width and height (values are in inches).
                double currentWidth = shape.XForm.Width.Value;
                double currentHeight = shape.XForm.Height.Value;

                // Apply scaling factor of 1.2 to both dimensions.
                double scaleFactor = 1.2;
                double newWidth = currentWidth * scaleFactor;
                double newHeight = currentHeight * scaleFactor;

                // Use the SetWidth and SetHeight methods to update the shape size.
                shape.SetWidth(newWidth);
                shape.SetHeight(newHeight);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shape {shapeId} resized and diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}