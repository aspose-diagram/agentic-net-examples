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

            // Specify the shape name whose ID you want to retrieve
            string shapeName = "MyShape";

            // Get the shape from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeName);

            // Retrieve the unique ID of the shape
            long shapeId = shape.ID;

            // Example usage: output the ID
            System.Console.WriteLine($"Shape ID for '{shapeName}' is {shapeId}");

            // Save the diagram if any modifications were made
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
