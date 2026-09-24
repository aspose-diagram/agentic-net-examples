using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape to move (example: shape with ID = 1 on the first page)
            int shapeId = 1;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Move the shape by setting new absolute coordinates (in inches)
            shape.XForm.PinX.Value = 5.0; // New PinX
            shape.XForm.PinY.Value = 3.0; // New PinY

            // Retrieve the updated PinX and PinY values to confirm the move
            double newPinX = shape.XForm.PinX.Value;
            double newPinY = shape.XForm.PinY.Value;

            Console.WriteLine($"Shape ID {shapeId} moved to PinX = {newPinX}, PinY = {newPinY}");

            // Save the diagram (optional, if you want to persist the changes)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
