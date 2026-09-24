using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Desired X coordinate (in inches)
            double newX = 5.0;

            // Set the X coordinate while preserving the existing Y coordinate
            shape.XForm.PinX.Value = newX;
            // Y coordinate remains unchanged: shape.XForm.PinY.Value

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
