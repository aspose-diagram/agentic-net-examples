using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // The ID of the shape we want to retrieve
            long shapeId = 12345; // example ID

            try
            {
                // Attempt to get the shape by ID from the first page
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                Console.WriteLine($"Shape found: ID={shape.ID}, NameU={shape.NameU}");
            }
            catch (Exception ex)
            {
                // Handle the case where the shape ID does not exist
                Console.WriteLine($"Error: Shape with ID {shapeId} was not found. Details: {ex.Message}");
            }

            // Optionally save the diagram after processing
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
