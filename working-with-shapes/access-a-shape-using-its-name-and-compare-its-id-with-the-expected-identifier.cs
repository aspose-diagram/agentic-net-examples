using System;
using System.IO;
using Aspose.Diagram;

class ShapeIdComparer
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (use the provided load rule in your environment)
            Diagram diagram = new Diagram("input.vsdx");

            // Define the shape name to locate and the expected identifier
            string targetShapeName = "MyShape";
            long expectedShapeId = 12345L;

            // Access the shape by its name from the first page
            Shape targetShape = diagram.Pages[0].Shapes.GetShape(targetShapeName);

            // Compare the retrieved shape's ID with the expected identifier
            if (targetShape != null && targetShape.ID == expectedShapeId)
            {
                System.Console.WriteLine("Shape ID matches the expected identifier.");
            }
            else
            {
                string actualId = targetShape != null ? targetShape.ID.ToString() : "null";
                System.Console.WriteLine($"Shape ID does not match. Actual ID: {actualId}");
            }

            // Save the diagram if further processing is required (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
