using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (active) page
            Page page = diagram.ActivePage;

            // Add a shape (e.g., a rectangle master) at position (2.0, 2.0)
            // The AddShape method returns the automatically assigned shape ID (long)
            long newShapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using the returned ID
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Output the ID for verification or further processing
            Console.WriteLine($"New shape ID: {newShapeId}");

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
