using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page
            Page page = diagram.Pages[0];

            // Retrieve the shape to be cloned (replace with actual shape ID)
            Shape originalShape = page.Shapes.GetShape(1);
            if (originalShape == null)
                throw new Exception("Original shape not found.");

            // Ensure the shape has an associated master (required for adding a new shape)
            if (originalShape.Master == null)
                throw new Exception("Original shape does not have a master.");

            string masterName = originalShape.Master.Name;

            // Add a new shape on the same page using the same master.
            // Position it slightly offset from the original shape.
            double offsetX = 2.0; // inches offset on X axis
            double newPinX = originalShape.XForm.PinX.Value + offsetX;
            double newPinY = originalShape.XForm.PinY.Value;

            long newShapeId = page.AddShape(newPinX, newPinY, masterName);
            Shape clonedShape = page.Shapes.GetShape(newShapeId);
            if (clonedShape == null)
                throw new Exception("Failed to create cloned shape.");

            // Inherit fill settings from the original shape
            clonedShape.Fill.FillPattern.Value = originalShape.Fill.FillPattern.Value;
            clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
            clonedShape.Fill.FillBkgnd.Value = originalShape.Fill.FillBkgnd.Value;

            // Modify the line inheritance flag by overriding the line color
            // (setting a value breaks inheritance and applies the new color)
            clonedShape.Line.LineColor.Value = "#FF0000"; // red line

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
