using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string inputPath = "example.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // The ID of the shape you want to select (replace with the actual ID)
            long shapeId = 10;

            // Access the first page of the diagram (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Output basic information about the selected shape
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Master Name: {(shape.Master != null ? shape.Master.Name : "None")}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
