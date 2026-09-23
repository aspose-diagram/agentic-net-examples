using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Define shape position and master name
            double pinX = 2.0;
            double pinY = 2.0;
            string masterName = "Rectangle";

            // Insert the shape; the method returns the shape ID (long)
            long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName, false);

            // Retrieve the shape object using the returned ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Set a solid fill pattern
            shape.Fill.FillPattern.Value = 1; // 1 = solid fill

            // Apply teal color (RGB 0,128,128) using a hex string
            shape.Fill.FillForegnd.Value = "#008080";

            // Save the diagram to VSDX format
            diagram.Save("TealShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
