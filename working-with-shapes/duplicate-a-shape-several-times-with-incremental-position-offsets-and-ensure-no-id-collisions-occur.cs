using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape to duplicate
            Shape originalShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    originalShape = shape;
                    break;
                }
            }

            if (originalShape == null)
            {
                Console.Error.WriteLine("No non-deleted shape found to duplicate.");
                return;
            }

            // Determine master name (fallback to a basic rectangle if master is missing)
            string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

            // Configuration for duplication
            int numberOfCopies = 5;          // How many duplicates to create
            double offsetX = 1.0;            // Horizontal offset per copy (in inches)
            double offsetY = 1.0;            // Vertical offset per copy (in inches)

            // Base position of the original shape
            double basePinX = originalShape.XForm.PinX.Value;
            double basePinY = originalShape.XForm.PinY.Value;

            // Duplicate the shape with incremental offsets
            for (int i = 1; i <= numberOfCopies; i++)
            {
                // Calculate new position
                double newPinX = basePinX + i * offsetX;
                double newPinY = basePinY + i * offsetY;

                // Add a new shape using the same master; AddShape returns the new shape ID (long)
                long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);

                // Retrieve the newly added shape
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Copy visual properties from the original shape to the new shape
                // (Assumes Shape.Copy method exists as per documentation)
                newShape.Copy(originalShape);

                // Ensure the new shape retains the intended position (Copy may overwrite it)
                newShape.XForm.PinX.Value = newPinX;
                newShape.XForm.PinY.Value = newPinY;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}