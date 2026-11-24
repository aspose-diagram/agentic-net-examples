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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page (page index 0).
                Page page = diagram.Pages[0];

                // Define the master name for a rectangle shape.
                string rectangleMasterName = "Rectangle";

                // Add a rectangle shape at an arbitrary position (e.g., PinX=2, PinY=2).
                // The fourth argument 'isCalculate' is set to false to avoid automatic layout.
                long shapeId = page.AddShape(2.0, 2.0, rectangleMasterName, false);

                // Retrieve the newly added shape using its ID.
                Shape rectangleShape = page.Shapes.GetShape(shapeId);

                // Set the width of the rectangle to 2 inches.
                rectangleShape.XForm.Width.Value = 2.0;

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