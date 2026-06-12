using System.IO;
using System;
using Aspose.Diagram;

class RetrieveShapeIdByName
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the shape name you want to find
            string shapeName = "MyShape";

            // Get the shape from the first page using its name
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeName);

            // Retrieve the unique ID of the shape
            long shapeId = shape.ID;

            // Display the result
            Console.WriteLine($"Shape \"{shapeName}\" has ID: {shapeId}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
