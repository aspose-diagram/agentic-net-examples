using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Offset to apply to the copied shape (in inches)
            double offsetX = 2.0;
            double offsetY = 1.0;

            // Iterate through shapes to find the hexagon master
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a master and that the master name is "Hexagon"
                if (shape.Master != null && shape.Master.Name == "Hexagon")
                {
                    // Original position
                    double origPinX = shape.XForm.PinX.Value;
                    double origPinY = shape.XForm.PinY.Value;

                    // New position with offset
                    double newPinX = origPinX + offsetX;
                    double newPinY = origPinY + offsetY;

                    // Add a new shape using the same master at the new location
                    long newShapeId = page.AddShape(newPinX, newPinY, shape.Master.Name, false);

                    // Retrieve the newly added shape to copy size (optional)
                    Shape newShape = page.Shapes.GetShape((int)newShapeId);
                    newShape.XForm.Width.Value = shape.XForm.Width.Value;
                    newShape.XForm.Height.Value = shape.XForm.Height.Value;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
