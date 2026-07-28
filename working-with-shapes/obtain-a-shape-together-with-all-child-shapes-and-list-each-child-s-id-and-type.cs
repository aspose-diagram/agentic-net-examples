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

            // Identify the shape you want to inspect (replace with the actual ID or name)
            int targetShapeId = 1; // example ID
            Shape targetShape = diagram.Pages[0].Shapes.GetShapeIncludingChild(targetShapeId);

            // Output the target shape's ID and type
            Console.WriteLine($"Shape ID: {targetShape.ID}, Type: {targetShape.Type}");

            // Recursively list all child shapes with their IDs and types
            ListChildShapes(targetShape);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void ListChildShapes(Shape parentShape)
    {
        // Iterate through direct child shapes
        foreach (Shape child in parentShape.Shapes)
        {
            Console.WriteLine($"Child Shape ID: {child.ID}, Type: {child.Type}");

            // If the child itself contains further children, process them recursively
            if (child.Shapes.Count > 0)
            {
                ListChildShapes(child);
            }
        }
    }
}
