using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page of the diagram.
            Page page = diagram.Pages[0];

            // Locate the first triangle shape on the page (master name "Triangle").
            Shape originalTriangle = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    originalTriangle = shape;
                    break;
                }
            }

            if (originalTriangle == null)
            {
                Console.Error.WriteLine("No triangle shape with master name 'Triangle' was found on the first page.");
                return;
            }

            // Retrieve original position and size.
            double origPinX = originalTriangle.XForm.PinX.Value;
            double origPinY = originalTriangle.XForm.PinY.Value;
            double shapeHeight = originalTriangle.XForm.Height.Value;
            double verticalSpacing = 0.5; // inches between duplicated shapes.

            // Duplicate the triangle three times and arrange them vertically below the original.
            for (int i = 1; i <= 3; i++)
            {
                double newPinX = origPinX;
                double newPinY = origPinY - i * (shapeHeight + verticalSpacing);

                // Add a new shape using the same master name.
                long newShapeId = page.AddShape(newPinX, newPinY, originalTriangle.Master.Name, false);

                // Retrieve the newly added shape (optional: further customization can be done here).
                Shape newShape = page.Shapes.GetShape(newShapeId);
                // Example: ensure the shape is not hidden (no direct hide property, so we keep dimensions as is).
                // Additional property copying can be performed here if needed.
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}