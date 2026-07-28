using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // The shape ID we want to retrieve
            long shapeId = 12345; // replace with the actual ID

            try
            {
                // Attempt to get the shape by its ID
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // If successful, work with the shape (example: output its name)
                Console.WriteLine($"Shape found: ID = {shape.ID}, Name = {shape.Name}");
            }
            catch (ArgumentException ex)
            {
                // Thrown when the shape ID does not exist in the collection
                Console.WriteLine($"Shape with ID {shapeId} was not found. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"An error occurred while retrieving the shape: {ex.Message}");
            }

            // Save the diagram (optional, depending on further processing)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
