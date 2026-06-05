using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first diamond shape on the page
            Shape? diamondShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Diamond")
                {
                    diamondShape = shape;
                    break;
                }
            }

            if (diamondShape == null)
            {
                throw new Exception("No diamond shape found on the page.");
            }

            // Retrieve the original shape's geometry
            double originalPinX = diamondShape.XForm.PinX.Value;
            double originalPinY = diamondShape.XForm.PinY.Value;
            double originalWidth = diamondShape.XForm.Width.Value;
            double originalHeight = diamondShape.XForm.Height.Value;

            // Add a duplicate of the diamond shape, offset 0.5 inches to the right
            double newPinX = originalPinX + 0.5; // offset by 0.5 inches
            long newShapeId = page.AddShape(newPinX, originalPinY, originalWidth, originalHeight, "Diamond");

            // (Optional) Retrieve the newly added shape if further modifications are needed
            // Shape newShape = page.Shapes.GetShape(newShapeId);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
