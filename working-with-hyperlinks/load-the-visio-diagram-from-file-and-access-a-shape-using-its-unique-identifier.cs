using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioFilePath = @"C:\Diagrams\sample.vsdx";

            // Unique identifier of the shape to retrieve
            long shapeId = 5; // replace with the actual shape ID

            // Load the Visio diagram from the file
            Diagram diagram = new Diagram(visioFilePath);

            // Access the first page (or any specific page as needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its unique ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example: output the shape's name
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
