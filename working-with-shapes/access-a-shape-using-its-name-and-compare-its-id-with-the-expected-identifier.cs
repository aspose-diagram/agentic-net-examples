using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the shape name to locate and the expected identifier
            string targetShapeName = "MyShape";
            long expectedShapeId = 12345L;

            // Retrieve the shape by its name from the first page
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeName);

            // Compare the retrieved shape's ID with the expected ID
            if (shape != null && shape.ID == expectedShapeId)
            {
                // IDs match – place your logic here
            }
            else
            {
                // IDs do not match or shape not found – handle accordingly
            }

            // Save the diagram if any modifications were made
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
