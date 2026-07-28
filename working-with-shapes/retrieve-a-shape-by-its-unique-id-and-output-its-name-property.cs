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
            Diagram diagram = new Diagram("input.vsdx"); // load rule

            // The unique ID of the shape you want to retrieve
            long shapeId = 5; // example ID; set to the desired value

            // Retrieve the shape by its ID from the first page
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Output the shape's Name property to the console
            Console.WriteLine($"Shape ID: {shapeId}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
