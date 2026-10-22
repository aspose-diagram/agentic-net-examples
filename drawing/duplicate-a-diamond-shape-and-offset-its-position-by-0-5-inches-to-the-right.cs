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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first diamond shape (master name "Diamond").
            Shape originalDiamond = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Identify diamond by its master name.
                if (shape.Master != null && shape.Master.Name == "Diamond")
                {
                    originalDiamond = shape;
                    break;
                }
            }

            if (originalDiamond == null)
            {
                Console.Error.WriteLine("No diamond shape found in the diagram.");
                return;
            }

            // Retrieve original geometry.
            double origPinX = originalDiamond.XForm.PinX.Value;
            double origPinY = originalDiamond.XForm.PinY.Value;
            double origWidth = originalDiamond.XForm.Width.Value;
            double origHeight = originalDiamond.XForm.Height.Value;

            // Offset position 0.5 inches to the right.
            double newPinX = origPinX + 0.5;
            double newPinY = origPinY;

            // Add a new diamond shape with the same size at the new position.
            long newShapeId = page.AddShape(newPinX, newPinY, origWidth, origHeight, "Diamond", false);

            // Retrieve the newly added shape.
            Shape newDiamond = page.Shapes.GetShape((int)newShapeId);

            // Copy text (if any) from the original diamond.
            string originalText = originalDiamond.Text.Value.Text;
            if (!string.IsNullOrEmpty(originalText))
            {
                // Clear any existing text runs.
                newDiamond.Text.Value.Clear();
                // Add a new text run with the original text.
                newDiamond.Text.Value.Add(new Txt(originalText));
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}