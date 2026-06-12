using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define the shape name you want to locate and the expected identifier
            string targetShapeName = "MyShape";
            long expectedShapeId = 12345;

            // Retrieve the shape by its name from the first page (adjust page index if needed)
            Aspose.Diagram.Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeName);

            // Compare the retrieved shape's ID with the expected identifier
            if (shape != null && shape.ID == expectedShapeId)
            {
                // The shape was found and its ID matches the expected value
                System.Console.WriteLine($"Shape '{targetShapeName}' has the expected ID: {expectedShapeId}.");
            }
            else
            {
                // Either the shape was not found or its ID does not match
                System.Console.WriteLine($"Shape '{targetShapeName}' not found or ID mismatch.");
            }

            // (Optional) Save the diagram if any modifications were made
            // diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
