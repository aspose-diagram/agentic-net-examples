using System.IO;
using System;
using Aspose.Diagram;

class CloneShapeExample
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Identify the shape to clone (e.g., shape with ID = 1)
            // Adjust the ID as needed for your specific diagram
            long sourceShapeId = 1;
            Shape sourceShape = page.Shapes.GetShape(sourceShapeId);

            // Create a new shape on the same page using the same master as the source shape
            // Position it initially at the same coordinates as the source shape
            double pinX = sourceShape.XForm.PinX.Value;
            double pinY = sourceShape.XForm.PinY.Value;
            string masterName = sourceShape.Master.NameU;

            long newShapeId = page.AddShape(pinX, pinY, masterName);
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Perform a deep copy of the source shape's properties into the new shape
            newShape.Copy(sourceShape);

            // Reposition the cloned shape (e.g., offset by 2 inches right and 1 inch down)
            double offsetX = 2.0; // inches
            double offsetY = 1.0; // inches
            newShape.MoveTo(pinX + offsetX, pinY + offsetY);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
