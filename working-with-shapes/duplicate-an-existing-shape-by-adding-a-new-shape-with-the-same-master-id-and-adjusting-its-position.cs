using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape to duplicate
                Shape sourceShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && shape.Master != null)
                    {
                        sourceShape = shape;
                        break;
                    }
                }

                if (sourceShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found to duplicate.");
                    return;
                }

                // Retrieve master name from the source shape
                string masterName = sourceShape.Master.Name;

                // Retrieve original position
                double originalPinX = sourceShape.XForm.PinX.Value;
                double originalPinY = sourceShape.XForm.PinY.Value;

                // Define offset for the duplicated shape (e.g., 1 inch right and down)
                double offsetX = 1.0;
                double offsetY = 1.0;

                // Add a new shape using the same master and adjusted position
                // page.AddShape returns the new shape's ID (long)
                long newShapeId = page.AddShape(originalPinX + offsetX, originalPinY + offsetY, masterName);

                // Retrieve the newly added shape instance
                Shape newShape = page.Shapes.GetShape((int)newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Optional: copy size from source shape
                newShape.XForm.Width.Value = sourceShape.XForm.Width.Value;
                newShape.XForm.Height.Value = sourceShape.XForm.Height.Value;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}