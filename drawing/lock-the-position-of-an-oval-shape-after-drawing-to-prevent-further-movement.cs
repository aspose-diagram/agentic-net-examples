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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Draw an oval (ellipse) on the page.
                // Parameters: pinX, pinY, width, height (all in inches).
                double pinX = 1.0;
                double pinY = 1.0;
                double width = 2.0;
                double height = 2.0;

                // DrawEllipse returns the shape ID (long).
                long shapeId = page.DrawEllipse(pinX, pinY, width, height);

                // Retrieve the newly created shape.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly created shape.");
                    return;
                }

                // Lock the shape's position to prevent movement.
                shape.Protection.LockMoveX.Value = BOOL.True;
                shape.Protection.LockMoveY.Value = BOOL.True;

                // Optionally, you can also lock width/height/rotation if desired.
                // shape.Protection.LockWidth.Value = BOOL.True;
                // shape.Protection.LockHeight.Value = BOOL.True;
                // shape.Protection.LockRotate.Value = BOOL.True;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}