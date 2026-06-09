using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "sample.vsdx";

            // Load the diagram from the file
            using (Diagram diagram = new Diagram(filePath))
            {
                // Get the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Unique identifier of the shape to retrieve
                long shapeId = 12345; // replace with the actual shape ID

                // Access the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Example usage: display shape information
                Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
