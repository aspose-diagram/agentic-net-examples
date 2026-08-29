using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the parent shape (by ID or name). Here we use an example ID = 1.
            int parentShapeId = 1;

            // Get the shape together with its child shapes.
            Shape parentShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(parentShapeId);

            // Iterate through all child shapes and output their ID and Type.
            foreach (Shape child in parentShape.Shapes)
            {
                Console.WriteLine($"Child ID: {child.ID}, Type: {child.Type}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
