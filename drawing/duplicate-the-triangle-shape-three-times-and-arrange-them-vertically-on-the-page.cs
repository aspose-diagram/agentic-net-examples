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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first triangle shape by its master name.
            Shape triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.Error.WriteLine("No triangle shape found in the diagram.");
                return;
            }

            // Store original geometry and text.
            double originalPinX = triangleShape.XForm.PinX.Value;
            double originalPinY = triangleShape.XForm.PinY.Value;
            double originalWidth = triangleShape.XForm.Width.Value;
            double originalHeight = triangleShape.XForm.Height.Value;
            string originalText = triangleShape.Text.Value.Text;

            // Define vertical spacing (shape height + a small gap).
            double verticalSpacing = originalHeight + 0.5; // 0.5 inches gap.

            // Duplicate the triangle three times and arrange vertically.
            for (int i = 1; i <= 3; i++)
            {
                // New Y position for the duplicate.
                double newPinY = originalPinY + i * verticalSpacing;

                // Add a new shape based on the same master.
                long newShapeId = page.AddShape(originalPinX, newPinY, triangleShape.Master.Name, false);
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Copy size.
                newShape.XForm.Width.Value = originalWidth;
                newShape.XForm.Height.Value = originalHeight;

                // Copy text.
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt(originalText));
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}