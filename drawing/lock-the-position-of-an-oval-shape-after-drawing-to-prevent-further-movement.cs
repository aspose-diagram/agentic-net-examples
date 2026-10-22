using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Draw an oval (ellipse) on the page.
            // Parameters: pinX, pinY, width, height (all in inches)
            double pinX = 2.0;   // X coordinate of the shape's center
            double pinY = 2.0;   // Y coordinate of the shape's center
            double width = 3.0;  // Width of the oval
            double height = 2.0; // Height of the oval

            // DrawEllipse returns the shape ID (long)
            long shapeId = page.DrawEllipse(pinX, pinY, width, height);

            // Retrieve the shape object using the returned ID
            Shape ovalShape = page.Shapes.GetShape(shapeId);
            if (ovalShape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly created oval shape.");
                return;
            }

            // Lock the position of the oval shape to prevent movement
            // LockMoveX and LockMoveY are BOOL cells; set their Value to BOOL.True
            ovalShape.Protection.LockMoveX.Value = BOOL.True;
            ovalShape.Protection.LockMoveY.Value = BOOL.True;

            // Optionally, you can also lock width/height if you want to prevent resizing
            // ovalShape.Protection.LockWidth.Value = BOOL.True;
            // ovalShape.Protection.LockHeight.Value = BOOL.True;

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}